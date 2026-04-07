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
            try
            {
                var suppliers = await _supplierService.GetAllSuppliersAsync();
                return View(suppliers);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public Task<IActionResult> Create()
        {
            try
            {
                return Task.FromResult<IActionResult>(View());
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _supplierService.AddSupplierAsync(supplier);
                    return RedirectToAction("Index");
                }
                return NotFound();
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
                var supplier = await _supplierService.GetSupplierByIdAsync(id);
                return View(supplier);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _supplierService.UpdateSupplierAsync(supplier);
                    return RedirectToAction("Index");
                }
                return NotFound();
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
                var supplier = await _supplierService.GetSupplierByIdAsync(id);
                return View(supplier);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Supplier supplier)
        {
            try
            {
                await _supplierService.RemoveSupplierAsync(supplier);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
