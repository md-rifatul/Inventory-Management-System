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

        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void AddCategory(Category categoryName);
        void RemoveCategory(Category categoryName);
        void UpdateCategory(Category categoryName);
    }
}
