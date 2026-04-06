using Inventory.Application.ViewModels;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product productName);
        Task RemoveProductAsync(int id);
        Task UpdateProductAsync(Product product);
        Task<IEnumerable<Product>> GetMinimumStockLevelsAsync();
        Task<IEnumerable<Product>> SearchProductsAsync(string search);
        Task AddStockAsync(int productId, int quantity);
        Task RemoveStockAsync(int productId, int quantity);
    }
}
