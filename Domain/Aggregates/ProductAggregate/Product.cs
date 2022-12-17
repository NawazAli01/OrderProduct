using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;
using ProductApis.Domain.Errors;
using ProductApis.Domain.Aggregates;
//using ProductApis.Domain.Aggregates.ProductAggregate;
using System;
using System.Net.Mail;
using ProductApis.Domain.Aggregates.OrderAggregate;

namespace ProductApis.Domain.Aggregates.ProductAggregate
{
    public class Product : Entity, IAggregateRoot
    {
        public int ProductId { get; private set; }
        public int OrderId { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public Quantity Quantity { get; private set; }
          
 
 //       public Order Order { get; private set; }
        public Product(int orderId, string name, Price price, Quantity quantity)
        {
            OrderId = orderId;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}




