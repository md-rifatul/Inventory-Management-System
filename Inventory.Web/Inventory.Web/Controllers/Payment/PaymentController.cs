using Inventory.Application.Services.Payment;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers.Payment
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

        public IActionResult Success(string session_id)
        {
            ViewBag.SessionId = session_id;
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
    }
}
