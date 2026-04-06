using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces.IServices
{
    public interface IOrderConfirmationService
    {
        Task<IEnumerable<SalesOrder>> GetAllSalesOrderAsync();
        Task<SalesOrder?> GetSalesOrderByIdAsync(int id);
        Task UpdateSalesOrderAsync(int id);
    }
}
