using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
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

        public IEnumerable<SalesOrder> GetAllSalesOrder()
        {
            return _salesOrderRepository.GetAllIncluding();
        }

        public SalesOrder GetSalesOrderById(int id)
        {
            return _salesOrderRepository.GetByIdIncluding(id, s=>s.SealsOrderItems);
        }

        public void UpdateSalesOrder(int id)
        {
            var order = GetSalesOrderById(id);
            order.SalesOrderStatus = SalesOrderStatus.Coompleted;
            _salesOrderRepository.Update(order);
            _salesOrderRepository.Save();
        }
    }
}
