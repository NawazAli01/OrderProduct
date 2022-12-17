using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApis.Domain.Aggregates.ProductAggregate;
using System.Reflection;
using Zero.EFCoreSpecification;
using ProductApis.Domain.Aggregates.ProductAggregate;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using ProductApis.Domain.Aggregates.OrderAggregate;
using ProductApis.Domain.Aggregates.ProductAggregate;

namespace ProductApis.Data
{
    public class ApplicationDbContext : DbContextBase<ApplicationDbContext>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options, mediator) { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
/*
            modelBuilder.Entity<Product>()
            .HasOne(o => o.Order)
            .HasForeignKey(p => p.Products);
            .ValueGeneratedNever();
*/
        }
       // public DbSet<OrderItem> order { get; set;}
    }
}
