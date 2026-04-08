using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.IO;

namespace Inventory.Web.Controllers.Payment
{
    public class WebhookController : Controller
    {
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
                    "YOUR_WEBHOOK_SECRET"
                );

                // 🎯 Handle payment success
                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Session;

                    var sessionId = session.Id;
                    var amount = session.AmountTotal / 100;
                    var email = session.CustomerDetails?.Email;

                    // ✅ TODO: Save to DB
                    Console.WriteLine($"Payment Success: {sessionId}");

                    // Example:
                    // var order = new Order { ... };
                    // _context.Add(order);
                    // await _context.SaveChangesAsync();
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
