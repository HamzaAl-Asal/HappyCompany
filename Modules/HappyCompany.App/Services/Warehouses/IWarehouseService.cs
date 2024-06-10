using HappyCompany.Abstraction.Models.Warehouses;
using Microsoft.AspNetCore.Http;

namespace HappyCompany.App.Services.Warehouses
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseViewModel>> GetWarehousesAsync();
        Task<WarehouseItemsViewModel> GetWarehouseWithItemsAsync(int warehouseId);
        Task<IResult> CreateWarehouseWithItemsAsync(WarehouseItemsViewModel warehouseItemsViewModel);
        Task<IResult> DeleteWarehouseByIdAsync(int warehouseId);
    }
}