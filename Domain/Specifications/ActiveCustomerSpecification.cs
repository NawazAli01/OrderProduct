using ProductApis.Domain.Aggregates.CustomerAggregate;
using Zero.SeedWorks;
using System;
using ProductApis.Domain.Aggregates.OrderAggregate;

namespace ProductApis.Domain.Specifications
{
    public class ActiveCustomerSpecification : BaseSpecification<Customer>
    {
        public ActiveCustomerSpecification() : base(m => !m.IsDeleted)
        {
        }
    }
}



