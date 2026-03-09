using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Application.Mappings;
using Inventory.Application.ViewModels;
using Inventory.Domain.Entities;
using Inventory.Domain.Entities.Enums;
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
        private readonly IStockTransactionService _stockTransactionService;
        public ProductService(IProductRepository productRepository, IStockTransactionService stockTransactionService)
        {
            _productRepository = productRepository;
            _stockTransactionService = stockTransactionService;
        }

        public void AddProduct(Product productName)
        {
            _productRepository.Add(productName);
            _productRepository.Save();
        }

        public void AddStock(int productId, int quantity)
        {
            if (quantity > 0)
            {
                var product = GetProductById(productId);
                if (product == null)
                    return;
                product.QuantityOfStock += quantity;
                var transaction = new StockTransaction
                {
                    ProductId = productId,
                    Quantity = quantity,
                    BlanceAfter = product.QuantityOfStock,
                    TransactionType = TransactionType.Purchase,
                };
                _stockTransactionService.Add(transaction);
                _productRepository.Save();
            }
            return;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _productRepository.GetAllIncluding(p => p.Category, s => s.Supplier);
            return products;
        }

        public IEnumerable<Product> GetMinimumStockLevels()
        {
            var products = _productRepository.GetAllIncluding(x=>x.Category,x=>x.Supplier)
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
