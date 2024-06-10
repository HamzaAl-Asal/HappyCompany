using AutoMapper;
using HappyCompany.Abstraction.Models.Warehouses;
using HappyCompany.App.Options;
using HappyCompany.Context;
using HappyCompany.Context.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HappyCompany.App.Services.Warehouses
{
    public class WarehouseService : IWarehouseService
    {
        private readonly HappyCompanyDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<WarehouseService> logger;
        private readonly IMemoryCache cache;
        private readonly CacheKeyOptions cacheKeyOptions;

        public WarehouseService(HappyCompanyDbContext context,
            IMapper mapper,
            ILogger<WarehouseService> logger,
            IMemoryCache cache,
            IOptionsMonitor<CacheKeyOptions> cacheKeyOptions)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
            this.cache = cache;
            this.cacheKeyOptions = cacheKeyOptions.CurrentValue;
        }

        public async Task<IEnumerable<WarehouseViewModel>> GetWarehousesAsync()
        {
            logger.LogInformation("--> Entering GetWarehousesAsync method.");

            List<WarehouseViewModel> warehousesViewModel;
            var warehousesCacheKey = cacheKeyOptions.Warehouses ?? string.Empty;

            if (!cache.TryGetValue(warehousesCacheKey, out warehousesViewModel))
            {
                logger.LogInformation("Fetching Warehouses from Database.");
                var warehouses = await context.Warehouses
                    .AsNoTracking()
                    .ToListAsync();

                warehousesViewModel = mapper.Map<List<WarehouseViewModel>>(warehouses);

                cache.Set(warehousesCacheKey, warehousesViewModel, TimeSpan.FromHours(1));

                logger.LogInformation("--> Exiting GetWarehousesAsync method with {Count} warehouses.", warehousesViewModel.Count());
                return warehousesViewModel;
            }

            logger.LogInformation("Fetching Warehouses from Cache.");

            if (warehousesViewModel == null)
            {
                logger.LogInformation("No warehouses found.");
                return Enumerable.Empty<WarehouseViewModel>();
            }

            logger.LogInformation("--> Exiting GetWarehousesAsync method with {Count} warehouses.", warehousesViewModel.Count());

            return warehousesViewModel;
        }

        public async Task<WarehouseItemsViewModel> GetWarehouseWithItemsAsync(int warehouseId)
        {
            logger.LogInformation("--> Entering GetWarehouseWithItemsAsync method with WarehouseId: {WarehouseId}.", warehouseId);

            WarehouseItemsViewModel warehouseItemsViewModel;
            var warehousePrefixKey = $"{cacheKeyOptions?.WarehousePrefix}{warehouseId}";
            if (!cache.TryGetValue(warehousePrefixKey, out warehouseItemsViewModel))
            {
                logger.LogInformation("Fetching Warehouse with items from Database.");
                var warehouseWithItems = await context.Warehouses
                    .Include(i => i.Items)
                    .SingleOrDefaultAsync(w => w.Id == warehouseId);

                warehouseItemsViewModel = mapper.Map<WarehouseItemsViewModel>(warehouseWithItems);

                cache.Set(warehousePrefixKey, warehouseItemsViewModel, TimeSpan.FromHours(1));

                logger.LogInformation("--> Exiting GetWarehouseWithItemsAsync method with {ItemCount} items.", warehouseItemsViewModel.Items.Count());

                return warehouseItemsViewModel;
            }

            logger.LogInformation("Fetching Warehouse with items from Cache.");

            if (warehouseItemsViewModel == null)
            {
                logger.LogWarning("Warehouse with Id: {WarehouseId} not found.", warehouseId);
                return new WarehouseItemsViewModel();
            }

            logger.LogInformation("--> Exiting GetWarehouseWithItemsAsync method with {ItemCount} items.", warehouseItemsViewModel.Items.Count());

            return warehouseItemsViewModel;
        }

        public async Task<IResult> CreateWarehouseWithItemsAsync(WarehouseItemsViewModel warehouseViewModel)
        {
            logger.LogInformation("--> Entering CreateWarehouseWithItemsAsync method with Warehouse Name: {WarehouseName}.", warehouseViewModel.Name);

            var warehouse = await context.Warehouses
                .AsNoTracking()
                .SingleOrDefaultAsync(w => w.Name == warehouseViewModel.Name);

            if (warehouse != null)
            {
                logger.LogError($"Warehouse with Name: '{warehouse.Name}' already exists!");
                logger.LogInformation("--> Exiting CreateWarehouseWithItemsAsync method.");

                return Results.BadRequest($"Warehouse with Name: '{warehouse.Name}' already exists!");
            }

            var existingItems = await GetExistingItemsAsync(warehouseViewModel.Items);
            if (existingItems.Any())
            {
                var existingItemsMessage = string.Join(", ", existingItems);

                logger.LogError($"Items with Names: '{existingItemsMessage}' already exist!");
                logger.LogInformation("--> Exiting CreateWarehouseWithItemsAsync method.");

                return Results.BadRequest($"Items with Names: '{existingItemsMessage}' already exist!");
            }

            var newWarehouse = mapper.Map<Warehouse>(warehouseViewModel);

            AddItemsToWarehouse(newWarehouse, warehouseViewModel.Items);

            context.Add(newWarehouse);
            await context.SaveChangesAsync();

            logger.LogInformation($"Warehouse with Name: {newWarehouse.Name} has been added successfully with ID: {newWarehouse.Id}");
            logger.LogInformation("--> Exiting CreateWarehouseWithItemsAsync method.");

            return Results.Ok("Warehouse has been added successfully!");
        }

        public async Task<IResult> DeleteWarehouseByIdAsync(int warehouseId)
        {
            logger.LogInformation($"--> Entering DeleteWarehouseByIdAsync method with Warehouse ID: '{warehouseId}'.");

            var warehouse = await context.Warehouses
                .Include(w => w.Items)
                .AsNoTracking()
                .SingleOrDefaultAsync(w => w.Id == warehouseId);

            if (warehouse == null)
            {
                logger.LogError($"Warehouse with ID: '{warehouseId}' does not exists !");
                logger.LogInformation("--> Exiting DeleteWarehouseByIdAsync method.");

                return Results.NotFound($"Warehouse with ID: '{warehouseId}' does not exists !");
            }

            context.Remove(warehouse);
            await context.SaveChangesAsync();

            logger.LogInformation($"Warehouse with Name: '{warehouse.Name}' has been deleted successfully with ID: '{warehouse.Id}'");
            logger.LogInformation("--> Exiting DeleteWarehouseByIdAsync method.");

            return Results.Ok("Warehouse has been deleted successfully !");
        }

        private async Task<List<string>> GetExistingItemsAsync(IEnumerable<ItemViewModel> items)
        {
            var existingItems = new List<string>();

            foreach (var itemViewModel in items)
            {
                var existingItem = await context.Items
                    .AsNoTracking()
                    .SingleOrDefaultAsync(i => i.Name == itemViewModel.Name);

                if (existingItem != null)
                {
                    existingItems.Add(existingItem.Name);
                }
            }

            return existingItems;
        }

        private void AddItemsToWarehouse(Warehouse newWarehouse, IEnumerable<ItemViewModel> items)
        {
            foreach (var itemViewModel in items)
            {
                var item = mapper.Map<Item>(itemViewModel);
                newWarehouse.Items.Add(item);
            }
        }
    }
}