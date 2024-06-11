using HappyCompany.Abstraction.Models;
using HappyCompany.Abstraction.Models.Warehouses;
using HappyCompany.App.Services.Warehouses;
using HappyCompany.Common;
using HappyCompany.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HappyCompany.Api.Endpoints.Warehouses
{
    public static class WarehouseEndpointExtensions
    {
        public static void MapWarehouseEndpoints(this IEndpointRouteBuilder builder)
        {
            var roles = $"{nameof(UserRole.Admin)}, {nameof(UserRole.Management)}, {nameof(UserRole.Auditor)}";

            builder.MapGet(Constant.GetWarehousesRouteName, HandleGetWarehousesAsync)
                .RequireAuthorization(Constant.WarehousePolicyName)
                .WithTags(Constant.WarehouseTagName);

            builder.MapGet(Constant.GetWarehouseByIdRouteName, HandleGetWarehouseWithItemsAsync)
                .RequireAuthorization(Constant.WarehousePolicyName)
                .WithTags(Constant.WarehouseTagName);

            builder.MapPost(Constant.CreateWarehouseRouteName, HandleCreateWarehouseWithItemsAsync)
                .RequireAuthorization(Constant.WarehousePolicyName)
                .WithTags(Constant.WarehouseTagName);

            builder.MapDelete(Constant.DeleteWarehouseByIdRouteName, HandleDeleteWarehouseByIdAsync)
                .RequireAuthorization(Constant.WarehousePolicyName)
                .WithTags(Constant.WarehouseTagName);
        }

        private static async Task<PaginationResult<WarehouseViewModel>> HandleGetWarehousesAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromServices] IWarehouseService warehouseService)
        {
            return await warehouseService.GetWarehousesAsync(pageNumber, pageSize);
        }

        private static async Task<WarehouseItemsPaginationResult> HandleGetWarehouseWithItemsAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromRoute] int warehouseId, [FromServices] IWarehouseService warehouseService)
        {
            return await warehouseService.GetWarehouseWithItemsAsync(pageNumber, pageSize, warehouseId);
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