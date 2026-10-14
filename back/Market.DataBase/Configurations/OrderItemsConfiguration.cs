using Market.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DataBase.Configurations
{
    public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);  
            builder.HasIndex(x => new { x.ProductId, x.OrderId })
                .IsUnique();
            builder.Property(x => x.Quantity).IsRequired();
        }
    }
}
