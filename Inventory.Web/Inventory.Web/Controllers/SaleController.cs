using Inventory.Application.Interfaces.IServices;
using Inventory.Application.ViewModels.Sales;
using Inventory.Domain.Entities;
using Inventory.Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stripe.V2;
using System.Threading.Tasks;

namespace Inventory.Web.Controllers
{
    public class SaleController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly ISalesOrderService _salesOrderService;
        public SaleController(IProductService productService, ISupplierService supplierService, ISalesOrderService salesOrderService)
        {
            _productService = productService;
            _supplierService = supplierService;
            _salesOrderService = salesOrderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct(int Id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(Id);
                if (product == null)
                    return NotFound();
                var vm = new CreateSalesOrderViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.UnitPrice
                };
                return View(vm);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetProuct(CreateSalesOrderViewModel createSalesOrderViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(createSalesOrderViewModel);
                }
                //await _salesOrderService.AddSaleOrderAsync(createSalesOrderViewModel);
                return RedirectToAction("Pay", "Payment", new { amount = createSalesOrderViewModel.Quantity*createSalesOrderViewModel.UnitPrice });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
