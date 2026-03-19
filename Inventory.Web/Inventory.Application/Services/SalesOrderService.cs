using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Application.ViewModels;
using Inventory.Domain.Entities;
using Inventory.Domain.Entities.Enums;

namespace Inventory.Application.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly ISalesOrderItemRepository _salesOrderItemRepository;
        public SalesOrderService(ISalesOrderRepository salesOrderRepository, ISalesOrderItemRepository salesOrderItemRepository)
        {
            _salesOrderRepository = salesOrderRepository;
            _salesOrderItemRepository = salesOrderItemRepository;
        }

        public void AddSaleOrder(CreateSalesOrderViewModel createSalesOrderViewModel)
        {

            //create sales order
            var salesOrder = new SalesOrder
            {
                OrderNumber = Guid.NewGuid().ToString(),
                CustomerName = createSalesOrderViewModel.CustomerName,
                TotalAmount = createSalesOrderViewModel.Quantity*createSalesOrderViewModel.UnitPrice,
                SalesOrderStatus = SalesOrderStatus.Pending
            };

            //create salesOrderItems
            var salesOrderItem = new SalesOrderItem
            {
                ProductId = createSalesOrderViewModel.ProductId,
                Quantity = createSalesOrderViewModel.Quantity,
                UnitPrice = createSalesOrderViewModel.UnitPrice
            };

            salesOrder.SealsOrderItems.Add(salesOrderItem);

            _salesOrderRepository.Add(salesOrder);
            _salesOrderRepository.Save();
        }
    }
}
