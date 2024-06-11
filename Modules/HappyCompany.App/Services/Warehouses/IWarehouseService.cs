using HappyCompany.Abstraction.Models;
using HappyCompany.Abstraction.Models.Warehouses;
using Microsoft.AspNetCore.Http;

namespace HappyCompany.App.Services.Warehouses
{
    public interface IWarehouseService
    {
        Task<PaginationResult<WarehouseViewModel>> GetWarehousesAsync(int pageNumber, int pageSize);
        Task<WarehouseItemsPaginationResult> GetWarehouseWithItemsAsync(int pageNumber, int pageSize, int warehouseId);
        Task<IResult> CreateWarehouseWithItemsAsync(WarehouseItemsViewModel warehouseItemsViewModel);
        Task<IResult> DeleteWarehouseByIdAsync(int warehouseId);
    }
}