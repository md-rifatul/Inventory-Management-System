using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Application.Mappings;
using Inventory.Application.ViewModels;
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
            var products = _productRepository.GetAllIncluding(p => p.Category, s => s.Supplier);
            return products;
            
        }

        public IEnumerable<Product> GetMinimumStockLevels()
        {
            var products = _productRepository.GetAllIncluding(x=>x.Category)
                .Where(x => x.QuantityOfStock <= x.MinimumStockLevel);
            return products;
        }

        public Product GetProductById(int id)
        {

            var product = _productRepository.GetByIdIncluding(id,c=>c.Category,p=>p.Supplier);
            return product;
        }

        public void RemoveProduct(int id)
        {
            var product = _productRepository.GetByIdIncluding(id);
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

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _productRepository.Save();
        }
    }
}
