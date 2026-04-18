using Inventory.Application.Interfaces.IRepository;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void RemoveRange()
        {
            throw new NotImplementedException();
        }
    }
}
