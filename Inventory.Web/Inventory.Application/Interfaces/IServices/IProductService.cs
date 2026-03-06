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
        void RemoveProduct(Product productName);
        void UpdateProduct(Product productName);
        IEnumerable<Product> GetMinimumStockLevels();
    }
}
