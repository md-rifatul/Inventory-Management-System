using AutoMapper;
using Inventory.Application.Interfaces.IServices;
using Inventory.Application.ViewModels.SalesOrder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderConfirmationService _orderConfirmationService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public OrderController(IOrderConfirmationService orderConfirmationService, IMapper mapper, IProductService productService)
        {
            _orderConfirmationService = orderConfirmationService;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var orders = await _orderConfirmationService.GetAllSalesOrderAsync();
                var vm = _mapper.Map<IEnumerable<SalesOrderSummaryViewModel>>(orders);
                return View(vm);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(int Id)
        {
            try
            {
                await _orderConfirmationService.UpdateSalesOrderAsync(Id);
                var order = await _orderConfirmationService.GetSalesOrderByIdAsync(Id);
                if (order == null)
                    return NotFound();

                foreach (var item in order.SealsOrderItems)
                {
                    await _productService.RemoveStockAsync(item.ProductId, item.Quantity);
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            try
            {
                var order = await _orderConfirmationService.GetSalesOrderByIdAsync(Id);
                var vm = _mapper.Map<SalesOrderConfirmViewModel>(order);
                return View(vm);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
