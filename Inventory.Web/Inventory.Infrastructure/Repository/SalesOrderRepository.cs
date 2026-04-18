using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Application.Interfaces.IRepository;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repository.Common;

namespace Inventory.Infrastructure.Repository
{
    public class SalesOrderRepository : Repository<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void RemoveRange(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
