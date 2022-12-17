using System.ComponentModel.DataAnnotations;
using Zero.SharedKernel.Constants;
using ProductApis.Domain.Aggregates.OrderAggregate;

namespace ProductApis.Models
{
    public class OrderRequestModel
    {
        [Required(ErrorMessage = "Customer ID Is Required.")]
        public int CustomerId { get; set; }
    }
}
