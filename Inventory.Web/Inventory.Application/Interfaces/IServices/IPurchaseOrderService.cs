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
        Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync();
        Task AddPurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task RemovePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task<PurchaseOrder?> GetPurchaseOrderAsync(int id);
    }
}
