using AutoMapper;
using HappyCompany.Abstraction.Models.Warehouses;
using HappyCompany.Context.DataAccess.Entities;

namespace HappyCompany.App.Services.Mappers
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