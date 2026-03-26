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
        public OrderController(IOrderConfirmationService orderConfirmationService, IMapper mapper)
        {
            _orderConfirmationService = orderConfirmationService;
            _mapper = mapper;
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
