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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var purchaseOrders = _purchaseOrderService.GetPurchaseOrder(id);
            var suppliers = _supplierService.GetAllSuppliers();
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
            return View(purchaseOrders);
        }
        [HttpPost]
        public IActionResult Edit(PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                _purchaseOrderService.UpdatePurchaseOrder(purchaseOrder);
                return RedirectToAction("Index");
            }
            var suppliers = _supplierService.GetAllSuppliers();
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
            return View(suppliers);

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var purchaseOrders = _purchaseOrderService.GetPurchaseOrder(id);
            var suppliers = _supplierService.GetAllSuppliers();
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
            return View(purchaseOrders);
        }
        [HttpPost]
        public IActionResult Delete(PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                _purchaseOrderService.RemovePurchaseOrder(purchaseOrder);
                return RedirectToAction("Index");
            }
            var suppliers = _supplierService.GetAllSuppliers();
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
            return View(suppliers);

        }
    }
}
