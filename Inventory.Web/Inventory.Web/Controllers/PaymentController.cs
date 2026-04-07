using Inventory.Application.Services.Payment;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public IActionResult Pay(decimal amount)
        {
            var session = _paymentService.CreateCheckoutSession(amount);
            return Redirect(session.Url);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
    }
}
