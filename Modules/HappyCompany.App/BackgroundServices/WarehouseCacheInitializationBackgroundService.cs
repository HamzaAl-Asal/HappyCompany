using HappyCompany.App.Options;
using HappyCompany.App.Services.Warehouses;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HappyCompany.App.BackgroundServices
{
    public class WarehouseCacheInitializationBackgroundService : BackgroundService
    {
        private readonly IMemoryCache cache;
        private readonly CacheKeyOptions cacheKeyOptions;
        private readonly IServiceScopeFactory scopeFactory;

        public WarehouseCacheInitializationBackgroundService(IMemoryCache cache,
            IOptionsMonitor<CacheKeyOptions> cacheKeyOptions,
            IServiceScopeFactory scopeFactory)
        {
            this.cache = cache;
            this.cacheKeyOptions = cacheKeyOptions.CurrentValue;
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await InitializeCache();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task InitializeCache()
        {
            using var scope = scopeFactory.CreateScope();
            var warehouseService = scope.ServiceProvider.GetRequiredService<IWarehouseService>();

            var warehousesKey = cacheKeyOptions?.Warehouses ?? string.Empty;

            var warehouses = await warehouseService.GetWarehousesAsync();
            cache.Set(warehousesKey, warehouses);

            foreach (var warehouse in warehouses)
            {
                var warehouseKey = $"{cacheKeyOptions?.WarehousePrefix}{warehouse.Id}";

                var warehouseWithItems = await warehouseService.GetWarehouseWithItemsAsync(warehouse.Id);
                cache.Set(warehouseKey, warehouseWithItems);
            }
        }
    }
}