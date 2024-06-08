using HappyCompany.Context.DataAccess.Entities;
using HappyCompany.Context.DataAccess.Entities.TypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HappyCompany.Context
{
    public class HappyCompanyDbContext : DbContext
    {
        private readonly IConfiguration confguration;
        public HappyCompanyDbContext(IConfiguration confguration)
        {
            this.confguration = confguration;
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(confguration.GetConnectionString(nameof(HappyCompanyDbContext)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseEntityTypeConfiguration).Assembly);
        }
    }
}