using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Inventory.Web.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ISupplierService _supplierService;
        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, ISupplierService supplierService, IProductService productService)
        {
            _purchaseOrderService = purchaseOrderService;
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var purchaseOrders = await _purchaseOrderService.GetAllPurchaseOrdersAsync();
                return View(purchaseOrders);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var suppliers = await _supplierService.GetAllSuppliersAsync();
                ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
                return View();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(PurchaseOrder purchaseOrder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _purchaseOrderService.AddPurchaseOrderAsync(purchaseOrder);
                    return RedirectToAction("Index");
                }
                var suppliers = await _supplierService.GetAllSuppliersAsync();
                ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
                return View(suppliers);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var purchaseOrders = await _purchaseOrderService.GetPurchaseOrderAsync(id);
                var suppliers = await _supplierService.GetAllSuppliersAsync();
                ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
                return View(purchaseOrders);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PurchaseOrder purchaseOrder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _purchaseOrderService.UpdatePurchaseOrderAsync(purchaseOrder);
                    return RedirectToAction("Index");
                }
                var suppliers = await _supplierService.GetAllSuppliersAsync();
                ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
                return View(suppliers);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var purchaseOrders = await _purchaseOrderService.GetPurchaseOrderAsync(id);
                var suppliers = await _supplierService.GetAllSuppliersAsync();
                ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
                return View(purchaseOrders);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(PurchaseOrder purchaseOrder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _purchaseOrderService.RemovePurchaseOrderAsync(purchaseOrder);
                    return RedirectToAction("Index");
                }
                var suppliers = await _supplierService.GetAllSuppliersAsync();
                ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
                return View(suppliers);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
