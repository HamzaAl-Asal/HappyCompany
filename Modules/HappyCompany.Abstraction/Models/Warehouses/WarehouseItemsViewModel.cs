namespace HappyCompany.Abstraction.Models.Warehouses
{
    public class WarehouseItemsViewModel : WarehouseViewModel
    {
        public IEnumerable<ItemViewModel> Items { get; set; }
    }
}