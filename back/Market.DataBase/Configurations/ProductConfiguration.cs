using Market.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DataBase.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Price).IsRequired();
            builder.HasMany(x => x.OrderItems)
                .WithOne(y => y.Product)
                .HasForeignKey(y => y.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
