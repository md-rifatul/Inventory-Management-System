using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Application.Interfaces.IRepository.Common;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces.IRepository
{
    public interface ISalesOrderRepository : IRepository<SalesOrder>
    {
        void RemoveRange(int Id);
    }
}
