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
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityOfStock { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
