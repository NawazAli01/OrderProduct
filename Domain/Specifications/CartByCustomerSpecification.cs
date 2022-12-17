using System;
using ProductApis.Domain.Aggregates.CustomerAggregate;
using Zero.SeedWorks;
using System.Linq.Expressions;
using ProductApis.Domain.Aggregates.CartAggregate;

namespace ProductApis.Domain.Specifications
{
    public class CartByCustomerSpecification : BaseSpecification<Cart>
    {
        public CartByCustomerSpecification(int Id) : base(m => m.CustomerId == Id)
        {
        }
    }
}

