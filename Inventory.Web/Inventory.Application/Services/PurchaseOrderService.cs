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
        public void AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _purchaseOrderRepository.Add(purchaseOrder);
            _purchaseOrderRepository.Save();
        }

        public PurchaseOrder GetPurchaseOrder(int id)
        {
            return _purchaseOrderRepository.GetByIdIncluding(id, s=>s.Supplier);
        }

        public IEnumerable<PurchaseOrder> GetAllPurchaseOrders()
        {
            return _purchaseOrderRepository.GetAllIncluding(s => s.Supplier);
        }

        public void RemovePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _purchaseOrderRepository.Delete(purchaseOrder);
            _purchaseOrderRepository.Save();
        }

        public void UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _purchaseOrderRepository.Update(purchaseOrder);
            _purchaseOrderRepository.Save();
        }
    }
}
