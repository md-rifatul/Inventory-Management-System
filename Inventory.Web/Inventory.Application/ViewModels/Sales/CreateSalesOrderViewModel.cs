using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.ViewModels.Sales
{
    public class CreateSalesOrderViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
