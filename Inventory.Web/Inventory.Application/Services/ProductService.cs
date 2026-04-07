using AutoMapper;
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

        public async Task AddProductAsync(Product productName)
        {
            try
            {
                _productRepository.Add(productName);
                await _productRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task AddStockAsync(int productId, int quantity)
        {
            try
            {
                if (quantity > 0)
                {
                    var product = await GetProductByIdAsync(productId);
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
                    await _stockTransactionService.AddAsync(transaction);
                    await _productRepository.SaveAsync();
                }
                return;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task RemoveStockAsync(int productId, int quantity)
        {
            try
            {
                if (quantity > 0)
                {
                    var product = await GetProductByIdAsync(productId);
                    if (product == null)
                        return;
                    product.QuantityOfStock -= quantity;
                    var transaction = new StockTransaction
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        BlanceAfter = product.QuantityOfStock,
                        TransactionType = TransactionType.Sale,
                    };
                    await _stockTransactionService.AddAsync(transaction);
                    await _productRepository.SaveAsync();
                }
                return;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productRepository.GetAllIncludingAsync(p => p.Category, s => s.Supplier);
                return products;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetMinimumStockLevelsAsync()
        {
            try
            {
                var products = await _productRepository.GetAllIncludingAsync(x => x.Category, x => x.Supplier);
                return products.Where(x => x.QuantityOfStock <= x.MinimumStockLevel);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdIncludingAsync(id, c => c.Category, p => p.Supplier);
                return product;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task RemoveProductAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdIncludingAsync(id);
                if (product == null)
                    return;
                _productRepository.Delete(product);
                await _productRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string search)
        {
            try
            {
                var products = await _productRepository.SearchAsync(
                    p => p.Name.Contains(search) || p.Sku.Contains(search), // predicate
                    p => p.Category,                                        // include
                    p => p.Supplier                                         // include
                );
                return products;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                _productRepository.Update(product);
                await _productRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
