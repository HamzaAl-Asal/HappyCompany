namespace HappyCompany.Common
{
    public static class Constant
    {
        // warehouse routes
        public const string WarehousesRouteName = "api/warehouses";
        public const string WarehouseByIdRouteName = "api/warehouses/{warehouseId}";

        // warehouse tag
        public const string WarehouseTagName = "Warehouse";

        public static class AppSettings
        {
            // options
            public const string CacheKeyOptions = "CacheKeyOptions";
        }
    }
}