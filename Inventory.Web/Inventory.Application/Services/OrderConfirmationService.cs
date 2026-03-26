using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;

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
            return _salesOrderRepository.GetByIdIncluding(id);
        }

        public void UpdateSalesOrder(SalesOrder salesOrder)
        {
            _salesOrderRepository.Update(salesOrder);
            _salesOrderRepository.Save();
        }
    }
}
