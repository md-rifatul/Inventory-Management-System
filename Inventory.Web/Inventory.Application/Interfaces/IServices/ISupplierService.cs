using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.IServices
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(int id);
        Task AddSupplierAsync(Supplier supplier);
        Task RemoveSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
    }
}
