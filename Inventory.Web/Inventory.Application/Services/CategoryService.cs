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
            try
            {
                _categoryRepository.Add(categoryName);
                await _categoryRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllIncludingAsync(p => p.Products);
                return categories.OrderBy(p => p.Name);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<Category?> GetCategoryByIdAsync(int id)
        {
            try
            {
                return _categoryRepository.GetByIdIncludingAsync(id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task RemoveCategoryAsync(Category categoryName)
        {
            try
            {
                _categoryRepository.Delete(categoryName);
                await _categoryRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task UpdateCategoryAsync(Category categoryName)
        {
            try
            {
                _categoryRepository.Update(categoryName);
                await _categoryRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
