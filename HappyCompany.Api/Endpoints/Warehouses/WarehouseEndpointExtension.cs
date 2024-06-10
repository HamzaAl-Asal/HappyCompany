using HappyCompany.Abstraction.Models.Warehouses;
using HappyCompany.App.Services.Warehouses;
using HappyCompany.Common;
using Microsoft.AspNetCore.Mvc;

namespace HappyCompany.Api.Endpoints.Warehouses
{
    public static class WarehouseEndpointExtension
    {
        public static void MapWarehouseEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet(Constant.WarehousesRouteName, HandleGetWarehousesAsync)
                .WithTags(Constant.WarehouseTagName);

            builder.MapGet(Constant.WarehouseByIdRouteName, HandleGetWarehouseWithItemsAsync)
                .WithTags(Constant.WarehouseTagName);

            builder.MapPost(Constant.WarehousesRouteName, HandleCreateWarehouseWithItemsAsync)
                .WithTags(Constant.WarehouseTagName);
            
            builder.MapDelete(Constant.WarehouseByIdRouteName, HandleDeleteWarehouseByIdAsync)
                .WithTags(Constant.WarehouseTagName);
        }

        private static async Task<IEnumerable<WarehouseViewModel>> HandleGetWarehousesAsync([FromServices] IWarehouseService warehouseService)
        {
            return await warehouseService.GetWarehousesAsync();
        }

        private static async Task<WarehouseItemsViewModel> HandleGetWarehouseWithItemsAsync([FromRoute] int warehouseId, [FromServices] IWarehouseService warehouseService)
        {
            return await warehouseService.GetWarehouseWithItemsAsync(warehouseId);
        }

        private static async Task<IResult> HandleCreateWarehouseWithItemsAsync([FromBody] WarehouseItemsViewModel warehouseItemsViewModel, [FromServices] IWarehouseService warehouseService)
        {
            return await warehouseService.CreateWarehouseWithItemsAsync(warehouseItemsViewModel);
        }

        private static async Task<IResult> HandleDeleteWarehouseByIdAsync([FromRoute] int warehouseId, [FromServices] IWarehouseService warehouseService)
        {
            return await warehouseService.DeleteWarehouseByIdAsync(warehouseId);
        }
    }
}