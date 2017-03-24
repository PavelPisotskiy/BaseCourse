using BaseCourse.Application.Models.Dto;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCourse.Application.Interfaces
{
    interface IOrderService : IDependency
    {
        OrderDto GetOrderById(string orderBusinessId);
        IEnumerable<OrderDto> GetOrders();
        OrderDto Create();
        void AddProductToOrder(string orderBusinessId, string productBusinessId, int quantity);
        void RemoveProductFromOrder(string orderBusinessId, string productBusinessId);
        void SetProductQuantity(string orderBusinessId, string productBusinessId, int quantity);
        void Checkout(string orderBusinessId);
        void Accept(string orderBusinessId);
        void Reject(string orderBusinessId);
    }
}
