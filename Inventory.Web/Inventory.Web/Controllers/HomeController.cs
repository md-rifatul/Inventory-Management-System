using System.Diagnostics;
using Inventory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inventory.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public Task<IActionResult> Index()
        {
            return Task.FromResult<IActionResult>(View());
        }

        public Task<IActionResult> Privacy()
        {
            return Task.FromResult<IActionResult>(View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public Task<IActionResult> Error()
        {
            return Task.FromResult<IActionResult>(View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));
        }
    }
}
