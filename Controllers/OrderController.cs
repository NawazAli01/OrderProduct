using Microsoft.AspNetCore.Mvc;
using Zero.SeedWorks;
using System;
using ProductApis.Domain.Specifications;
using ProductApis.Models;
using ProductApis.Domain.Aggregates;
using Zero.AspNetCoreServiceProjectExample.Domain;
using ProductApis.Domain.Aggregates.OrderAggregate;
using ProductApis.Domain.Aggregates.CartAggregate;
using ProductApis.Domain.Aggregates.ProductAggregate;
using System.Reflection.Metadata.Ecma335;

namespace ProductApis.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Cart> _cartRepository;
        public OrderController(IRepository<Order> orderRepository, IRepository<Cart> cartRepository)
        {
            this._orderRepository = orderRepository;
            this._cartRepository = cartRepository;
        }

        [ProducesResponseType(typeof(OrderResponseModel), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var order = await _orderRepository.ListAllAsync();
            return Ok(order.Select(m => new OrderResponseModel
            {
                //add whatever u added in orderResponseModel
                OrderId = m.OrderId,
                CustomerId = m.CustomerId,
                
            }));
        }



        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<IActionResult> PostAsync(OrderRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var cartitem = await _cartRepository.ListAsync(new CartByCustomerSpecification(model.CustomerId));
                if (cartitem == null) return NotFound("Customer Id is not valid");
                //var order = new Order(model.productId, model.orderId, model.name, model.price, model.quantity, model.description);
                List<OrderItem> items = new List<OrderItem>();
                foreach (var cart in cartitem)
                {
                    var abc = new OrderItem(cart.ProductId, cart.Name, cart.Price, cart.Quantity);
                    items.Add(abc);
                }
                int totalqty = 0, totalprice = 0;
                foreach (var item in items)
                {
                    totalqty += item.Quantity;
                    totalprice += item.Price;
                }
                var cartItem = new Order(model.CustomerId, totalqty, totalprice, items);
                await _orderRepository.AddAsync(cartItem);
                await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                return Ok(cartItem);
            }
            return ValidationProblem(ModelState); 
        } 
        

    }
}
