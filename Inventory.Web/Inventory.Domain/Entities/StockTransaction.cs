using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int BlanceAfter { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
