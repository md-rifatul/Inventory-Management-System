using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Application.ViewModels.Sales;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces.IServices
{
    public interface ISalesOrderService
    {
        void AddSaleOrder(CreateSalesOrderViewModel createSalesOrderViewModel);
    }
}
