using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Stripe;

[ApiController]
public class WebhookController : ControllerBase
{
    private readonly ISalesOrderService _salesOrderService;
    private readonly IConfiguration _configuration;

    public WebhookController(ISalesOrderService salesOrderService, IConfiguration configuration)
    {
        _salesOrderService = salesOrderService;
        _configuration = configuration;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _configuration["Stripe:WebhookSecret"]
            );

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Stripe.Checkout.Session;

                var orderId = session?.Metadata["orderId"];

                if (int.TryParse(orderId, out int id))
                {
                    await _salesOrderService.UpdateOrderStatusAsync(id, SalesOrderStatus.Paid);
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