using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApis.Domain.Aggregates.ProductAggregate;
using ProductApis.Models;
using ProductApis.Domain.Aggregates;
using ProductApis.Domain.Aggregates.OrderAggregate;
using ProductApis.Domain.Aggregates.CustomerAggregate;
using System.Net.Mail;
using System;
//using ProductApis.Domain.Specifications;

namespace ProductApis.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId);

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(c => c.CustomerId);

  
            
/*            builder.Property(x => x.)
                .IsUnicode(true)
                .IsRequired();

            builder.HasMany<Product>(c => c.Products)
                .WithOne(e => e.Order);
            //doubt
*/
        }
    }
}


