using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Application.Mappings;
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
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public void AddProduct(ProductCreateViewModel productName)
        {   var product = _mapper.Map<Product>(productName);
            _productRepository.Add(product);
            _productRepository.Save();
        }

        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            var products = _productRepository.GetAllIncluding(p => p.Category, s => s.Supplier);
            var vm = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return vm;
            
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
