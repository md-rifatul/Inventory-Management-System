using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.IServices
{
    public interface IPurchaseOrderService
    {
        IEnumerable<PurchaseOrder> GetAllPurchaseOrders();
        void AddPurchaseOrder(PurchaseOrder purchaseOrder);
        void RemovePurchaseOrder(PurchaseOrder purchaseOrder);
        void UpdatePurchaseOrder(PurchaseOrder purchaseOrder);
        PurchaseOrder GetPurchaseOrder(int id);
    }
}
