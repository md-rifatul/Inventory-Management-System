using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.ViewModels;
using Inventory.Domain.Entities;
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
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel,Product>();

            CreateMap<ProductCreateViewModel, Product>();

            CreateMap<ProductEditViewModel, Product>();

            CreateMap<ProductDeleteViewModel, Product>();
            CreateMap<Product,ProductDeleteViewModel>();

            CreateMap<Product, ProductViewLowStock>();

            CreateMap<Product, AddStockViewModel>()
    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
    .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.QuantityOfStock));
        }
    }
}
