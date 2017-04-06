using BaseCourse.Application.Models.Dto;
using BaseCourse.Domain.Models.Entities;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCourse.Application.Interfaces
{
    public interface IOrderService : IDependency
    {
        OrderDto GetCart();
        IEnumerable<OrderDto> GetCurrentCustomerOrders();
        OrderDto Get(string orderBusinessId);
        IEnumerable<OrderDto> GetAllProcessingOrders();
        void AddToCart(string productId);
        void RemoveFromCart(string productId);
        void SetProductQuantity(string productBusinessId, int quantity);
        void Checkout(string orderId);
        void Reject(string orderId);
        void Accept(string orderId);
        double GetCartTotalPrice();
    }
}
