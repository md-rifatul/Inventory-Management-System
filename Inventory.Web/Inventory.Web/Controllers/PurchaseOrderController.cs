using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Web.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ISupplierService _supplierService;
        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, ISupplierService supplierService)
        {
            _purchaseOrderService = purchaseOrderService;
            _supplierService = supplierService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var purchaseOrders = _purchaseOrderService.GetAllPurchaseOrders();
            return View(purchaseOrders);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                _purchaseOrderService.AddPurchaseOrder(purchaseOrder);
                return RedirectToAction("Index");
            }
            var suppliers = _supplierService.GetAllSuppliers();
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
            return View(suppliers);

        }
    }
}
