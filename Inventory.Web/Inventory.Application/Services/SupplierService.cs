using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            _supplierRepository.Add(supplier);
            await _supplierRepository.SaveAsync();
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _supplierRepository.GetAllAsync();
        }

        public Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            return _supplierRepository.GetByIdIncludingAsync(id);
        }

        public async Task RemoveSupplierAsync(Supplier supplier)
        {
            _supplierRepository.Delete(supplier);
            await _supplierRepository.SaveAsync();
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            _supplierRepository.Update(supplier);
            await _supplierRepository.SaveAsync();
        }
    }
}
