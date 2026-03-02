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
            return _productRepository.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void RemoveProduct(Product productName)
        {
            _productRepository.Delete(productName);
            _productRepository.Save();
        }

        public void UpdateProduct(Product productName)
        {
            _productRepository.Update(productName);
            _productRepository.Save();
        }
    }
}
