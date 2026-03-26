using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.ViewModels.Products
{
    public class ProductEditViewModel : ProductCreateViewModel
    {
        public int Id { get; set; }

    }
}
