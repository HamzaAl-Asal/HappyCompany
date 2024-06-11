using AutoMapper;
using HappyCompany.Abstraction.Models.Warehouses;
using HappyCompany.Context.DataAccess.Entities.Warehouses;

namespace HappyCompany.App.Services.Mappers.WarehouseProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemViewModel>();

            CreateMap<ItemViewModel, Item>();
        }
    }
}