using ProductApis.Domain.Aggregates.ProductAggregate;
using Zero.SeedWorks;

namespace ProductApis.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public Quantity Quantity { get; private set; }

        private OrderItem() { }
        public OrderItem(int productId, string name, Price price, Quantity quantity) { 
        
            ProductId = productId;   
            Name = name;
            Price = price;
            Quantity = quantity;        
        }
    }
}


