using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategoryAsync(Category categoryName)
        {
            _categoryRepository.Add(categoryName);
            await _categoryRepository.SaveAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllIncludingAsync(p => p.Products);
            return categories.OrderBy(p => p.Name);
        }

        public Task<Category?> GetCategoryByIdAsync(int id)
        {
            return _categoryRepository.GetByIdIncludingAsync(id);
        }

        public async Task RemoveCategoryAsync(Category categoryName)
        {
            _categoryRepository.Delete(categoryName);
            await _categoryRepository.SaveAsync();
        }

        public async Task UpdateCategoryAsync(Category categoryName)
        {
            _categoryRepository.Update(categoryName);
            await _categoryRepository.SaveAsync();
        }
    }
}
