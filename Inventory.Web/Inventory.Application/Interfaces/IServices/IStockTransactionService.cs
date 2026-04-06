using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.IServices
{
    public interface IStockTransactionService
    {
        Task<IEnumerable<StockTransaction>> GetStockTransactionsAsync();
        Task AddAsync(StockTransaction transaction);
    }
}
