using Inventory.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    public class StockTransactionController : Controller
    {
        private readonly IStockTransactionService _stockTransactionService;
        private readonly IProductService _productService;
        public StockTransactionController(IStockTransactionService stockTransactionService, IProductService productService)
        {
            _stockTransactionService = stockTransactionService;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var stockTransations = _stockTransactionService.GetStockTransactions();
            var products = _productService.GetAllProducts();
            return View(stockTransations);
        }
    }
}