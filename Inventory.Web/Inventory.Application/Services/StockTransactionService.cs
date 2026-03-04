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

        public void Add(StockTransaction transaction)
        {
            _stockTransactionRepository.Add(transaction);
            _stockTransactionRepository.Save();
        }

        public IEnumerable<StockTransaction> GetStockTransactions()
        {
            return _stockTransactionRepository.GetAllIncluding(x => x.Product);
        }
    }
}
