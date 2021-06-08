using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleShop.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Infrastructure.Persistence.Configurations
{
    class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product), Constants.Database.Schema);

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(300);

            builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500);

            builder.Property(x => x.Price)
               .IsRequired()
               .HasColumnType("money");

        }
    }
}
