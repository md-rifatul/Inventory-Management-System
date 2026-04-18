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
    public class StockTransactionService : IStockTransactionService
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;
        public StockTransactionService(IStockTransactionRepository stockTransactionRepository)
        {
            _stockTransactionRepository = stockTransactionRepository;
        }

        public async Task AddAsync(StockTransaction transaction)
        {
            try
            {
                _stockTransactionRepository.Add(transaction);
                await _stockTransactionRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<StockTransaction>> GetStockTransactionsAsync()
        {
            try
            {
                return await _stockTransactionRepository.GetAllIncludingAsync(x => x.Product);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
