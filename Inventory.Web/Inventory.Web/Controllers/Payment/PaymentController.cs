using Inventory.Application.Interfaces.IServices;
using Inventory.Application.ViewModels.Sales;
using Microsoft.AspNetCore.Mvc;
using Inventory.Domain.Entities.Enums;
using Stripe.Checkout;

public class PaymentController : Controller
{
    private readonly ISalesOrderService _salesOrderService;
    private readonly IPaymentService _paymentService;

    public PaymentController(ISalesOrderService salesOrderService, IPaymentService paymentService)
    {
        _salesOrderService = salesOrderService;
        _paymentService = paymentService;
    }

    public async Task<IActionResult> Pay(CreateSalesOrderViewModel model)
    {
        var totalAmount = model.Quantity * model.UnitPrice;

        // ✅ Save Order first
        var salesOrder = await _salesOrderService.AddSalesOrderAsync(model);

        // ✅ Create Stripe session with metadata
        var session = _paymentService.CreateCheckoutSession(totalAmount, salesOrder.Id);

        return Redirect(session.Url);
    }

    public async Task<IActionResult> Success(string session_id)
    {
        if (!string.IsNullOrWhiteSpace(session_id))
        {
            var sessionService = new SessionService();
            var session = await sessionService.GetAsync(session_id);

            if (
                session?.PaymentStatus == "paid"
                && session.Metadata.TryGetValue("orderId", out var orderIdText)
                && int.TryParse(orderIdText, out var orderId)
            )
            {
                await _salesOrderService.UpdateOrderStatusAsync(orderId, SalesOrderStatus.Paid);
            }
        }

        ViewBag.SessionId = session_id;
        return View();
    }

    public IActionResult Cancel()
    {
        return View();
    }
}