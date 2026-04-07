using Inventory.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            try
            {
                var stockTransations = await _stockTransactionService.GetStockTransactionsAsync();
                var products = await _productService.GetAllProductsAsync();
                return View(stockTransations);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}