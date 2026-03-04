using Inventory.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var purchaseOrders = _purchaseOrderService.GetAllPurchaseOrders();
            return View(purchaseOrders);
        }
    }
}
