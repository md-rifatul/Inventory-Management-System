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
    public class OrderConfirmationRepository : Repository<SalesOrder>, IOrderConfirmationRepository
    {
        public OrderConfirmationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void RemoveRange()
        {
            throw new NotImplementedException();
        }
    }
}
