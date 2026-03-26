using AutoMapper;
using Inventory.Application.ViewModels;
using Inventory.Application.ViewModels.Products;
using Inventory.Application.ViewModels.Sales;
using Inventory.Application.ViewModels.SalesOrder;
using Inventory.Domain.Entities;
using Inventory.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SupplierName,
                    opt => opt.MapFrom(src => src.Supplier.Name));

            CreateMap<ProductViewModel,Product>();

            CreateMap<ProductCreateViewModel, Product>();

            CreateMap<ProductEditViewModel, Product>();
            CreateMap<Product,ProductEditViewModel>();

            CreateMap<ProductDeleteViewModel, Product>();
            CreateMap<Product,ProductDeleteViewModel>();

            CreateMap<Product, ProductViewLowStock>();

            CreateMap<Product, AddStockViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.QuantityOfStock));


            CreateMap<CreateSalesOrderViewModel, SalesOrder>()
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice))
                .ForMember(dest => dest.SalesOrderStatus, opt => opt.MapFrom(src => SalesOrderStatus.Pending));

            CreateMap<CreateSalesOrderViewModel, SalesOrderItem>();

            CreateMap<SalesOrder, SalesOrderSummaryViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.SalesOrderStatus.ToString()));
        }
    }
}
