using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
using Inventory.Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Web.Controllers
{
    public class SaleController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        public SaleController(IProductService productService, ISupplierService supplierService)
        {
            _productService = productService;
            _supplierService = supplierService;
        }
        [HttpGet]
        public IActionResult CreateSale()
        {
            var sale = new PurchaseOrder
            {
                OrderNumber = Guid.NewGuid().ToString(),
                purchaseOrderStatus = PurchaseOrderStatus.Pending
            };

            var suppliers = _supplierService.GetAllSuppliers();

            ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
            return View(sale);
        }

        //[HttpPost]
        //public IActionResult PendingSale(PurchaseOrder purchaseOrder)
        //{
        //    purchaseOrder.purchaseOrderStatus = PurchaseOrderStatus.Pending;
        //}
    }
}
