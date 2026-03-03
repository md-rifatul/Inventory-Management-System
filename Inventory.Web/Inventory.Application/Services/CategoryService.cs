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

        public void AddCategory(Category categoryName)
        {
            _categoryRepository.Add(categoryName);
            _categoryRepository.Save();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetByIdIncluding(id);
        }

        public void RemoveCategory(Category categoryName)
        {
            _categoryRepository.Delete(categoryName);
            _categoryRepository.Save();
        }

        public void UpdateCategory(Category categoryName)
        {
            _categoryRepository.Update(categoryName);
            _categoryRepository.Save();
        }
    }
}
