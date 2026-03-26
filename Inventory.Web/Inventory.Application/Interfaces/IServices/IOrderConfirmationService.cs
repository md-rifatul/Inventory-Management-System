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
        IEnumerable<SalesOrder> GetAllSalesOrder();
        SalesOrder GetSalesOrderById(int id);
        void UpdateSalesOrder(int id);
    }
}
