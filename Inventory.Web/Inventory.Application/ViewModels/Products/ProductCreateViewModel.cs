using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.ViewModels.Products
{
    public class ProductCreateViewModel
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? QuantityOfStock { get; set; }
        public int? MinimumStockLevel { get; set; }

        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public List<SelectListItem>? Suppliers { get; set; }
    }
}
