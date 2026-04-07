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
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return View(categories);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public Task<IActionResult> Add()
        {
            try
            {
                return Task.FromResult<IActionResult>(View());
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(Category categoryName)
        {
            try
            {
                await _categoryService.AddCategoryAsync(categoryName);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);

                return View(category);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category != null)
                    await _categoryService.RemoveCategoryAsync(category);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                return View(category);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditConfirm(Category newCategory)
        {
            try
            {
                var existingCategory = await _categoryService.GetCategoryByIdAsync(newCategory.Id);
                if (existingCategory == null)
                    return NotFound();
                existingCategory.Name = newCategory.Name;
                existingCategory.Description = newCategory.Description;
                await _categoryService.UpdateCategoryAsync(existingCategory);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
