using Market.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DataBase.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasIndex(x => x.NumberId).IsUnique();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(500);
            builder.HasMany(x => x.OrderItems)
                .WithOne(y => y.Order)
                .HasForeignKey(y => y.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
