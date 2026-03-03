using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int QuantityOfStock { get; set; }

        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
