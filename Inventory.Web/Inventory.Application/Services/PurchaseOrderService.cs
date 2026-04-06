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
            _purchaseOrderRepository.Add(purchaseOrder);
            await _purchaseOrderRepository.SaveAsync();
        }

        public Task<PurchaseOrder?> GetPurchaseOrderAsync(int id)
        {
            return _purchaseOrderRepository.GetByIdIncludingAsync(id, s => s.Supplier);
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync()
        {
            return await _purchaseOrderRepository.GetAllIncludingAsync(s => s.Supplier);
        }

        public async Task RemovePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            _purchaseOrderRepository.Delete(purchaseOrder);
            await _purchaseOrderRepository.SaveAsync();
        }

        public async Task UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            _purchaseOrderRepository.Update(purchaseOrder);
            await _purchaseOrderRepository.SaveAsync();
        }
    }
}
