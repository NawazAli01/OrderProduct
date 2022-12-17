using System.ComponentModel.DataAnnotations;
using Zero.SharedKernel.Constants;
using ProductApis.Domain.Aggregates.CartAggregate;
using ProductApis.Domain.Aggregates;
using ProductApis.Domain.Aggregates.ProductAggregate;

namespace ProductApis.Models
{
    public class CartRequestModel
    {
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

    }
}
