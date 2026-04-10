using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.IO;

[ApiController]
public class WebhookController : Controller
{
    private readonly ISalesOrderService _salesOrderService;
    private readonly IConfiguration _configuration;

    public WebhookController(ISalesOrderService salesOrderService, IConfiguration configuration)
    {
        _salesOrderService = salesOrderService;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("webhook")]
    public async Task<IActionResult> Index()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _configuration["Stripe:WebhookSecret"]
            );

            // ✅ Payment Success
            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;
                if (
                    session != null
                    && session.Metadata.TryGetValue("orderId", out var orderId)
                    && !string.IsNullOrWhiteSpace(orderId)
                    && int.TryParse(orderId, out var parsedOrderId)
                )
                {
                    await _salesOrderService.UpdateOrderStatusAsync(
                        parsedOrderId,
                        SalesOrderStatus.Paid
                    );
                }
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}