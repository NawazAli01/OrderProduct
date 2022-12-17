using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApis.Domain.Aggregates.ProductAggregate;
using ProductApis.Models;
using ProductApis.Domain.Aggregates;
//using ProductApis.Domain.Specifications;
using System.Net.Mail;
using System;
using ProductApis.Domain.Aggregates.OrderAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApis.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.Name);
      
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


        }
    }
}



