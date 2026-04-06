using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }
        public Task<IActionResult> Add()
        {
            return Task.FromResult<IActionResult>(View());
        }
        [HttpPost]
        public async Task<IActionResult> Add(Category categoryName)
        {
            await _categoryService.AddCategoryAsync(categoryName);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category != null)
                await _categoryService.RemoveCategoryAsync(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> EditConfirm(Category newCategory)
        {
            var existingCategory = await _categoryService.GetCategoryByIdAsync(newCategory.Id);
            if (existingCategory == null)
                return NotFound();
            existingCategory.Name = newCategory.Name;
            existingCategory.Description = newCategory.Description;
            await _categoryService.UpdateCategoryAsync(existingCategory);
            return RedirectToAction("Index");
        }
    }
}
