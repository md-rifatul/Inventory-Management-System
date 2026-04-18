using Inventory.Application.Interfaces.IRepository.Common;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void RemoveRange();
    }
}
