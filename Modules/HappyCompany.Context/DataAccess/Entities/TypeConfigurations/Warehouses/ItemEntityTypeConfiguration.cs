﻿using HappyCompany.Context.DataAccess.Entities.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyCompany.Context.DataAccess.Entities.TypeConfigurations.Warehouses
{
    public class ItemEntityTypeConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(w => w.Name)
                 .IsUnique();

            builder.Property(i => i.SKUCode);

            builder.Property(i => i.Qty)
                   .IsRequired();

            builder.Property(i => i.CostPrice)
                   .IsRequired();

            builder.Property(i => i.MSRPPrice);
        }
    }
}