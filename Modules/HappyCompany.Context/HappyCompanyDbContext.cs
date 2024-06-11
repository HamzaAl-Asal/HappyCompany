using HappyCompany.Common.Enums;
using HappyCompany.Context.DataAccess.Entities.TypeConfigurations.Warehouses;
using HappyCompany.Context.DataAccess.Entities.Users;
using HappyCompany.Context.DataAccess.Entities.Warehouses;
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

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(confguration.GetConnectionString(nameof(HappyCompanyDbContext)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseEntityTypeConfiguration).Assembly);

            RolesSeedData(modelBuilder);
            AdminUserSeedData(modelBuilder);
        }

        private static void RolesSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = nameof(UserRole.Admin) },
                new Role { Id = 2, Name = nameof(UserRole.Management) },
                new Role { Id = 3, Name = nameof(UserRole.Auditor) }
            );
        }

        private static void AdminUserSeedData(ModelBuilder modelBuilder)
        {
            var adminUser = new User
            {
                Id = 1,
                Email = "admin@happywarehouse.com",
                FullName = "Administrator",
                RoleId = 1,
                IsActive = true,
                Password = "P@ssw0rd"
            };

            modelBuilder.Entity<User>().HasData(adminUser);
        }
    }
}