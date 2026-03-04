using Inventory.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    public class StockTransactionController : Controller
    {
        private readonly IStockTransactionService _stockTransactionService;
        public StockTransactionController(IStockTransactionService stockTransactionService)
        {
            _stockTransactionService = stockTransactionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var stockTransations = _stockTransactionService.GetStockTransactions();
            return View(stockTransations);
        }
    }
}