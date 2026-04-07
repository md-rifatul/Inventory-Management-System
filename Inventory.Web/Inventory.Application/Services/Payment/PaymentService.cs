using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace Inventory.Application.Services.Payment
{
    public class PaymentService
    {
        private readonly IConfiguration _config;

        public PaymentService(IConfiguration config)
        {
            _config = config;
            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
        }

        public Session CreateCheckoutSession(decimal amount)
        {
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
                                Name = "Test Product"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = "https://your-ngrok-url/payment/success",
                CancelUrl = "https://your-ngrok-url/payment/cancel"
            };

            var service = new SessionService();
            return service.Create(options);
        }
    }
}