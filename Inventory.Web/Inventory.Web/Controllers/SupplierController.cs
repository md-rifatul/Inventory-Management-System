using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inventory.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return View(suppliers);
        }
        public Task<IActionResult> Create()
        {
            return Task.FromResult<IActionResult>(View());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierService.AddSupplierAsync(supplier);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            return View(supplier);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierService.UpdateSupplierAsync(supplier);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Supplier supplier)
        {
            await _supplierService.RemoveSupplierAsync(supplier);
            return RedirectToAction("Index");
        }

    }
}
