using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.ViewModels.SalesOrder
{
    public class SalesOrderConfirmViewModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }

        // Items list for the admin to verify stock/contents
        public List<OrderItemDetailViewModel> Items { get; set; } = new();
    }
}
