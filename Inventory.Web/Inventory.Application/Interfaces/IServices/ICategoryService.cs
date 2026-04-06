using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.IServices
{
    public interface ICategoryService
    {

        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category categoryName);
        Task RemoveCategoryAsync(Category categoryName);
        Task UpdateCategoryAsync(Category categoryName);
    }
}
