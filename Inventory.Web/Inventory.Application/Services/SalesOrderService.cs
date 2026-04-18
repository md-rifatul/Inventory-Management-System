using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Application.ViewModels.Sales;
using Inventory.Domain.Entities;
using Inventory.Domain.Entities.Enums;

namespace Inventory.Application.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly ISalesOrderItemRepository _salesOrderItemRepository;
        private readonly IMapper _mapper;
        public SalesOrderService(ISalesOrderRepository salesOrderRepository, ISalesOrderItemRepository salesOrderItemRepository, IMapper mapper)
        {
            _salesOrderRepository = salesOrderRepository;
            _salesOrderItemRepository = salesOrderItemRepository;
            _mapper = mapper;
        }

        public async Task<SalesOrder> AddSalesOrderAsync(CreateSalesOrderViewModel createSalesOrderViewModel)
        {
            var salesOrder = _mapper.Map<SalesOrder>(createSalesOrderViewModel);
            var salesOrderItem = _mapper.Map<SalesOrderItem>(createSalesOrderViewModel);

            salesOrder.SealsOrderItems.Add(salesOrderItem);

            _salesOrderRepository.Add(salesOrder);
            await _salesOrderRepository.SaveAsync();

            return salesOrder;
        }


        public async Task<SalesOrder?> SalesOrderAsyncById(int id)
        {
            return await _salesOrderRepository.GetByIdIncludingAsync(id, s => s.SealsOrderItems);
        }

        public async Task UpdateOrderStatusAsync(int orderId, SalesOrderStatus status)
        {
            var order = await _salesOrderRepository.GetByIdIncludingAsync(orderId);

            if (order == null)
            {
                throw new Exception("Sales order not found");
            }

            order.SalesOrderStatus = status;

            await _salesOrderRepository.SaveAsync();
        }
    }
}
