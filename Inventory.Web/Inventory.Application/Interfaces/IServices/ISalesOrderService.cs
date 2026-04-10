using Inventory.Application.ViewModels.Sales;
using Inventory.Domain.Entities;
using Inventory.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.IServices
{
    public interface ISalesOrderService
    {
        Task<SalesOrder> AddSalesOrderAsync(CreateSalesOrderViewModel createSalesOrderViewModel);
        Task<SalesOrder?> SalesOrderAsyncById(int id);
        Task UpdateOrderStatusAsync(int orderId, SalesOrderStatus status);
    }
}
