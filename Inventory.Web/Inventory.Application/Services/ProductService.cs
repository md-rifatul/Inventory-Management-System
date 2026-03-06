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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddProduct(Product productName)
        {
            _productRepository.Add(productName);
            _productRepository.Save();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllIncluding(p => p.Category, s => s.Supplier);
        }

        public IEnumerable<Product> GetMinimumStockLevels()
        {
            var products = _productRepository.GetAllIncluding(x=>x.Category)
                .Where(x => x.QuantityOfStock <= x.MinimumStockLevel);
            return products;
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetByIdIncluding(id,c=>c.Category,p=>p.Supplier);
        }

        public void RemoveProduct(Product product)
        {
            _productRepository.Delete(product);
            _productRepository.Save();
        }

        public IEnumerable<Product> SearchProducts(string search)
        {
            var products = _productRepository.Search(
                p => p.Name.Contains(search) || p.Sku.Contains(search), // predicate
                p => p.Category,                                        // include
                p => p.Supplier                                         // include
            );
            return products;
        }

        public void UpdateProduct(Product productName)
        {
            _productRepository.Update(productName);
            _productRepository.Save();
        }
    }
}
