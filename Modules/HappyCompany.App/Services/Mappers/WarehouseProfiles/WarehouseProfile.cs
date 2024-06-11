using AutoMapper;
using HappyCompany.Abstraction.Models.Warehouses;
using HappyCompany.Context.DataAccess.Entities.Warehouses;

namespace HappyCompany.App.Services.Mappers.WarehouseProfiles
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