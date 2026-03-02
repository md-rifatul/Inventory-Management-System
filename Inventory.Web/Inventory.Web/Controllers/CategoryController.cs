using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Category categoryName)
        {
            _categoryService.AddCategory(categoryName);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            return View(category);
        }
        [HttpPost]
        public IActionResult RemoveConfirmed(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            _categoryService.RemoveCategory(category);
            return RedirectToAction("Index");
        }
    }
}
