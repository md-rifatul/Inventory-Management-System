using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Entities.Enums;

namespace Inventory.Application.Services
{
    public class OrderConfirmationService : IOrderConfirmationService
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        public OrderConfirmationService(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }

        public async Task<IEnumerable<SalesOrder>> GetAllSalesOrderAsync()
        {
            try
            {
                return await _salesOrderRepository.GetAllIncludingAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<SalesOrder?> GetSalesOrderByIdAsync(int id)
        {
            try
            {
                return _salesOrderRepository.GetQueryable()
                .Include(s => s.SealsOrderItems)      // Level 1: Get the list of items
                .ThenInclude(i => i.Product)      // Level 2: Get the Product for EACH item
            .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task UpdateSalesOrderAsync(int id)
        {
            try
            {
                var order = await GetSalesOrderByIdAsync(id);
                if (order == null)
                    return;
                order.SalesOrderStatus = SalesOrderStatus.Completed;
                _salesOrderRepository.Update(order);
                await _salesOrderRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
