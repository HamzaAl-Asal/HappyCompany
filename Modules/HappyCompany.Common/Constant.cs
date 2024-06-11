namespace HappyCompany.Common
{
    public static class Constant
    {
        // warehouse routes
        public const string GetWarehousesRouteName = "api/warehouses";
        public const string GetWarehouseByIdRouteName = "api/warehouses/{warehouseId}";
        public const string CreateWarehouseRouteName = "api/warehouses/create";
        public const string DeleteWarehouseByIdRouteName = "api/warehouses/delete/{warehouseId}";
       
        // warehouse tags
        public const string WarehouseTagName = "Warehouse";

        // warehouse policy name
        public const string WarehousePolicyName = "WarehousePolicy";
        
        // user routes
        public const string UsersLoginRouteName = "api/users/login";
        public const string EditUserRouteName = "api/users/edit";
        public const string ChangeUserPasswordRouteName = "api/users/change-password";
        public const string DeleteUserRouteName = "api/users/delete/{userId}";

        // user policy name
        public const string UserActionsPolicyName = "UserActionsPolicy";

        // user tags
        public const string UserTagName = "User";

        public static class AppSettings
        {
            public const string CacheKeyOptions = "CacheKeyOptions";
            public const string JWTAuthenticationOptions = "JWTAuthenticationOptions";
        }
    }
}