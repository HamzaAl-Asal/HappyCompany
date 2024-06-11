namespace HappyCompany.Abstraction.Models.Warehouses
{
    public class WarehouseItemsPaginationResult
    {
        public WarehouseViewModel Warehouse { get; set; }
        public PaginationResult<ItemViewModel> ItemsPaginationResult { get; set; }
    }
}