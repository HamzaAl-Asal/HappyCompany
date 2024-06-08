using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyCompany.Context.DataAccess.Entities.TypeConfigurations
{
    public class WarehouseEntityTypeConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Name)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(w => w.Address)
                   .IsRequired();

            builder.Property(w => w.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(w => w.Country)
                   .IsRequired()
            .HasMaxLength(100);


            builder
                 .HasMany(s => s.Items)
                 .WithMany(c => c.Warehouses)
                 .UsingEntity(j => j.ToTable("WarehouseItems"));
        }
    }
}