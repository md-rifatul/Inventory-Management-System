using Inventory.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = Guid.NewGuid().ToString();
        public decimal TotalAmount { get; set; }
        public PurchaseOrderStatus purchaseOrderStatus { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItems>();
    }
}
