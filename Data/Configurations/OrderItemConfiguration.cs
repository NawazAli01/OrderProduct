using Microsoft.EntityFrameworkCore;
using ProductApis.Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApis.Domain.Aggregates.ProductAggregate;
using ProductApis.Models;
using ProductApis.Domain.Aggregates;
using ProductApis.Domain.Aggregates.CustomerAggregate;
using System.Net.Mail;
using System;

namespace ProductApis.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.HasKey(x => x.OrderItemId);

            builder.Property(x => x.Name)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(x => x.Price)
              .HasConversion(x => x.Value, x => Price.Create(x).Value)
              .HasMaxLength(15)
              .IsUnicode(true)
              .IsRequired();

            builder.Property(x => x.Quantity)
              .HasConversion(x => x.Value, x => Quantity.Create(x).Value)
              .HasMaxLength(15)
              .IsUnicode(true)
              .IsRequired();

            builder.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId);

        }
    }
}

