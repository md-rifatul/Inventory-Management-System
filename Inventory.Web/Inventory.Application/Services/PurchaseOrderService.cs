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
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }
        public async Task AddPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            try
            {
                _purchaseOrderRepository.Add(purchaseOrder);
                await _purchaseOrderRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<PurchaseOrder?> GetPurchaseOrderAsync(int id)
        {
            try
            {
                return _purchaseOrderRepository.GetByIdIncludingAsync(id, s => s.Supplier);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync()
        {
            try
            {
                return await _purchaseOrderRepository.GetAllIncludingAsync(s => s.Supplier);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task RemovePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            try
            {
                _purchaseOrderRepository.Delete(purchaseOrder);
                await _purchaseOrderRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            try
            {
                _purchaseOrderRepository.Update(purchaseOrder);
                await _purchaseOrderRepository.SaveAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
