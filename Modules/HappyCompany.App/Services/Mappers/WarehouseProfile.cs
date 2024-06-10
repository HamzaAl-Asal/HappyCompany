using AutoMapper;
using HappyCompany.Abstraction.Models.Warehouses;
using HappyCompany.Context.DataAccess.Entities;

namespace HappyCompany.App.Services.Mappers
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            CreateMap<Warehouse, WarehouseViewModel>()
                .ReverseMap();

            CreateMap<Warehouse, WarehouseItemsViewModel>();

            CreateMap<WarehouseItemsViewModel, Warehouse>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());
        }
    }
}