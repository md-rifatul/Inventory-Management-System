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
            try
            {
                _supplierRepository.Add(supplier);
                await _supplierRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            try
            {
                return await _supplierRepository.GetAllAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            try
            {
                return _supplierRepository.GetByIdIncludingAsync(id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task RemoveSupplierAsync(Supplier supplier)
        {
            try
            {
                _supplierRepository.Delete(supplier);
                await _supplierRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            try
            {
                _supplierRepository.Update(supplier);
                await _supplierRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
