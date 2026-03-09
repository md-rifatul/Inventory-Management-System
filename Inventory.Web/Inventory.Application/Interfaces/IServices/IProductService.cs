using Inventory.Application.DTOs;
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
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product productName);
        void RemoveProduct(int id);
        void UpdateProduct(Product product);
        IEnumerable<Product> GetMinimumStockLevels();
        IEnumerable<Product> SearchProducts(string search);
        void AddStock(int productId, int quantity);
    }
}
