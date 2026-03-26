using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityOfStock { get; set; }
        public int MinimumStockLevel { get; set; }

        public string CategoryName { get; set; }
        public string SupplierName { get; set; }

    }
}
