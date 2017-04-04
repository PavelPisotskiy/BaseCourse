using BaseCourse.Domain.Models.Enums;
using BaseCourse.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Domain.Models.Entities
{
    public class Order
    {
        private readonly int customerId;
        public int CustomerId { get { return customerId; } }

        private readonly string orderBusinessId;
        public string OrderBusinessId { get { return orderBusinessId; } }

        public DateTime PlacingDateUtc { get; private set; }

        public Status Status { get; private set; }

        public IEnumerable<OrderItem> OrderItems { get { return orderItems; } }

        public double TotalPrice { get; private set; }

        private readonly List<OrderItem> orderItems = new List<OrderItem>();

        private Order(int customerId, string orderBusinessId, DateTime placingDateUtc, Status status, IEnumerable<OrderItem> orderItems, double totalPrice)
        {
            this.customerId = customerId;
            this.orderBusinessId = orderBusinessId;
            PlacingDateUtc = placingDateUtc;
            Status = status;
            this.orderItems.AddRange(orderItems);
            TotalPrice = totalPrice;
        }

        public Order(int customerId, string orderBusinessId)
            : this(customerId, orderBusinessId, DateTime.UtcNow, Status.Open, Enumerable.Empty<OrderItem>(), 0)
        {

        }


        public void AddProduct(string productBusinessId, int quantity)
        {
            if (Status != Status.Open)
            {
                throw new InvalidOperationException("You can add product only in 'Open' status");
            }

            var orderItem = orderItems.FirstOrDefault(o => o.ProductBusinessId.Equals(productBusinessId));
            if (orderItem == null)
            {
                orderItem = new OrderItem(orderBusinessId, productBusinessId, quantity);
                orderItems.Add(orderItem);
            }
            else
            {
                SetProductQuantity(productBusinessId, orderItem.Quantity + quantity);
            }
        }

        public void RemoveProduct(string productBusinessId)
        {
            if (Status != Status.Open)
            {
                throw new InvalidOperationException("You can remove product only in 'Open' status");
            }

            OrderItem orderItem = orderItems.FirstOrDefault(o => o.ProductBusinessId.Equals(productBusinessId));
            if (orderItem == null)
            {
                throw new ArgumentException("OrderItem not found.", "productBusinessId");
            }

            orderItems.Remove(orderItem);
        }

        public void Checkout(IPriceCalculator priceCalculator)
        {
            if (orderItems.Count == 0)
            {
                throw new InvalidOperationException("You order is empty.");
            }

            if (Status != Status.Open)
            {
                throw new InvalidOperationException("You can checkout only in 'Open' status");
            }

            PlacingDateUtc = DateTime.UtcNow;
            Status = Status.Processing;
            TotalPrice = priceCalculator.Calculate(this);
        }

        public void Accept()
        {
            if (Status != Status.Processing)
            {
                throw new InvalidOperationException("You can accept order only in 'Processing' status");
            }

            Status = Enums.Status.Accepted;
        }

        public void Reject()
        {
            if (Status != Status.Processing)
            {
                throw new InvalidOperationException("You can reject order only in 'Processing' status");
            }

            Status = Enums.Status.Rejected;
        }

        public void SetProductQuantity(string productBusinessId, int quantity)
        {
            if (Status != Status.Open)
            {
                throw new InvalidOperationException("You can change product quantity only in 'Open' status");
            }

            OrderItem orderItem = orderItems.FirstOrDefault(_orderItem => _orderItem.ProductBusinessId.Equals(productBusinessId));
            if (orderItem == null)
            {
                throw new ArgumentException("Product with id: " + productBusinessId + " is not found", "productBusinessId");
            }
            OrderItem newOrderItem = new OrderItem(orderBusinessId, productBusinessId, quantity);
            orderItems.Remove(orderItem);
            orderItems.Add(newOrderItem);
        }

        public static Order FromPersistence(int customerId, string orderBusinessId, DateTime placingDateUtc, 
            Status status, IEnumerable<OrderItem> orderItems, double totalPrice)
        {
            return new Order(customerId, orderBusinessId, placingDateUtc, status, orderItems, totalPrice);
        }

    }
}