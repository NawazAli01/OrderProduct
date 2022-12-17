using ProductApis.Domain.Aggregates.OrderAggregate;

namespace ProductApis.Models
{
    public class OrderResponseModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> items { get; set; }
    }
}




