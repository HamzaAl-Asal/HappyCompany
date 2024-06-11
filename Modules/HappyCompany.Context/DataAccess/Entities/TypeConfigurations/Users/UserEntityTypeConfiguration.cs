using HappyCompany.Context.DataAccess.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyCompany.Context.DataAccess.Entities.TypeConfigurations.Users
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .IsRequired();

            builder.HasOne(u => u.Role)
                  .WithMany()
                  .HasForeignKey(u => u.RoleId);
        }
    }
}