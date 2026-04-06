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

        public async Task AddSaleOrderAsync(CreateSalesOrderViewModel createSalesOrderViewModel)
        {
            //create sales order
            var salesOrder = _mapper.Map<SalesOrder>(createSalesOrderViewModel);

            //create salesOrderItems
            var salesOrderItem = _mapper.Map<SalesOrderItem>(createSalesOrderViewModel);


            salesOrder.SealsOrderItems.Add(salesOrderItem);

            _salesOrderRepository.Add(salesOrder);
            await _salesOrderRepository.SaveAsync();
        }
    }
}
