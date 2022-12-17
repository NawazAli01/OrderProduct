using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zero.SeedWorks;
using ProductApis.Domain.Aggregates.ProductAggregate;
using ProductApis.Domain.Aggregates;
using Zero.AspNetCoreServiceProjectExample.Domain;

namespace ProductApis.Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public int OrderId { get; private set; }
        public int CustomerId { get; private set; }
        public int TotalQuantity {  get; private set; }
        public int TotalPrice { get; private set; }

        private List<OrderItem> _items = new List<OrderItem>();
        public IReadOnlyList<OrderItem> items => _items.AsReadOnly();
       
        public Order(int customerId, int totalQuantity, int totalPrice, List<OrderItem> items) {        
            CustomerId = customerId;
            TotalQuantity = totalQuantity;
            TotalPrice = totalPrice;
            _items = items;
        }

        private Order()
        {

        }

/*
        public void AddItem(int productId, int orderId, string name, Price price, Quantity quantity, string description)
        {
            _items.Add(new OrderItem(productId, orderId, name, price, quantity, description));
        } */
    }
}

