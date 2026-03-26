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
    }
}
