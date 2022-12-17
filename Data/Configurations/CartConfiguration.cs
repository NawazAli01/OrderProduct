using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net.Mail;
using System;
using ProductApis.Domain.Aggregates.CartAggregate;
using ProductApis.Domain.Aggregates.ProductAggregate;
using ProductApis.Models;
using ProductApis.Domain.Aggregates;
using ProductApis.Domain.Aggregates.OrderAggregate;
using ProductApis.Domain.Aggregates.CustomerAggregate;

namespace ProductApis.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.CartId);
            
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId);
            
            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId);
                
            builder.Property(x => x.Name)
                .IsUnicode(true)
                .IsRequired();
            
            builder.Property(x => x.Quantity)
              .HasConversion(x => x.Value, x => Quantity.Create(x).Value)
              .HasMaxLength(15)
              .IsUnicode(true)
              .IsRequired();
           
            builder.Property(x => x.Price)
               .HasConversion(x => x.Value, x => Price.Create(x).Value)
               .HasMaxLength(15)
               .IsUnicode(true)
               .IsRequired();
        }
    }
}
