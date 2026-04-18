using Inventory.Application.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace Inventory.Application.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;

        public PaymentService(IConfiguration config)
        {
            _config = config;
            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
        }

        public Session CreateCheckoutSession(decimal amount, int orderId)
        {
            var baseUrl = _config["AppSettings:BaseUrl"] ?? "https://localhost:44320";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(amount * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Order Payment"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",

                Metadata = new Dictionary<string, string>
                {
                    {"orderId",orderId.ToString() }
                },

                SuccessUrl = $"{baseUrl}/Payment/Success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{baseUrl}/Payment/Cancel",
            };

            var service = new SessionService();
            return service.Create(options);
        }
    }
}