using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zero.SeedWorks;
using ProductApis.Domain.Aggregates.ProductAggregate;
using ProductApis.Domain.Errors;
using Zero.AspNetCoreServiceProjectExample.Domain;
using Zero.SharedKernel.Types.Result;

namespace ProductApis.Domain.Aggregates.CartAggregate
{
    public class Cart : Entity, IAggregateRoot
    {
        public int CartId { get; set; }
        public int CustomerId { get; private set; }
        public int ProductId { get; private set; }   //Product Id
        public string Name { get; private set; }
        public Quantity Quantity { get; private set; }
        public Price Price { get; private set; }
        public bool IsDeleted { get; private set; }
        public Cart(int customerId, int productId, string name, Quantity quantity, Price price) {
            CustomerId = customerId;
            ProductId = productId;   
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public Result Update(string name, Quantity quantity, string description, Price price)
        {
            if (IsDeleted) return Result.Failure(new DeletedCartError("Cart products Deleted can not be updated."));

            
            Name = name;
            Quantity = quantity;
            Price = price;

            return Result.Success();
        }

        public void Delete()
        {
            IsDeleted = true;
        }

    }
}
