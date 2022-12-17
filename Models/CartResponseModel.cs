using ProductApis.Domain.Aggregates.ProductAggregate;
using ProductApis.Domain.Aggregates;

namespace ProductApis.Models
{
    public class CartResponseModel
    {
        public int customerId { get; set; }
        public int productId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }
}
