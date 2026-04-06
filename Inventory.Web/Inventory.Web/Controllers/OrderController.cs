using AutoMapper;
using Inventory.Application.Interfaces.IServices;
using Inventory.Application.ViewModels.SalesOrder;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            var orders = _orderConfirmationService.GetAllSalesOrder();
            var vm = _mapper.Map<IEnumerable<SalesOrderSummaryViewModel>>(orders);
            return View(vm);
        }
        [HttpPost]
        public IActionResult ConfirmOrder(int Id)
        {
            _orderConfirmationService.UpdateSalesOrder(Id);
            var order = _orderConfirmationService.GetSalesOrderById(Id);

            foreach (var item in order.SealsOrderItems)
            {
                _productService.RemoveStock(item.ProductId, item.Quantity);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var order = _orderConfirmationService.GetSalesOrderById(Id);
            var vm = _mapper.Map<SalesOrderConfirmViewModel>(order);
            return View(vm);
        }
    }
}
