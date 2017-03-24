using BaseCourse.Application.Interfaces;
using BaseCourse.Application.Mappers;
using BaseCourse.Application.Models.Dto;
using BaseCourse.Domain;
using BaseCourse.Domain.Interfaces;
using BaseCourse.Domain.Models.Entities;
using BaseCourse.Domain.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderStorage orderStorage;
        private readonly IProductStorage productStorage;
        private readonly OrderDtoMapper orderDtoMapper;
        private readonly IUserService userService;

        public OrderService(IOrderStorage orderStorage, IProductStorage productStorage, 
            IUserService userService)
        {
            this.orderStorage = orderStorage;
            this.productStorage = productStorage;
            this.userService = userService;
            orderDtoMapper = new OrderDtoMapper();
        }

        public OrderDto GetOrderById(string orderBusinessId)
        {
            Order order = orderStorage.GetByBusinessId(orderBusinessId);
            OrderDto orderDto = orderDtoMapper.GetOrderDto(order);
            return orderDto;
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            IEnumerable<OrderDto> orders = orderStorage.GetOrders().Select(order => orderDtoMapper.GetOrderDto(order));
            return orders;
        }

        public OrderDto Create()
        {
            string orderBusinessId = Guid.NewGuid().ToString();
            int currentUserId = userService.GetCurrentUser().Id;
            Order newOrder = new Order(currentUserId, orderBusinessId);
            orderStorage.Create(newOrder);
            return orderDtoMapper.GetOrderDto(newOrder);
        }
        
        public void Checkout(string orderBusinessId)
        {
            IPriceCalculator calculator = new PriceCalculator(productStorage);
            Order order = orderStorage.GetByBusinessId(orderBusinessId);
            order.Checkout(calculator);
            orderStorage.Update(order);
        }

        public void Accept(string orderBusinessId)
        {
            Order order = orderStorage.GetByBusinessId(orderBusinessId);
            order.Accept();
            orderStorage.Update(order);
        }

        public void Reject(string orderBusinessId)
        {
            Order order = orderStorage.GetByBusinessId(orderBusinessId);
            order.Reject();
            orderStorage.Update(order);
        }

        public void AddProductToOrder(string orderBusinessId, string productBusinessId, int quantity)
        {
            Order order = orderStorage.GetByBusinessId(orderBusinessId);
            order.AddProduct(productBusinessId, quantity);
            orderStorage.Update(order);
        }

        public void RemoveProductFromOrder(string orderBusinessId, string productBusinessId)
        {
            Order order = orderStorage.GetByBusinessId(orderBusinessId);
            order.RemoveProduct(productBusinessId);
            orderStorage.Update(order);
        }

        public void SetProductQuantity(string orderBusinessId, string productBusinessId, int quantity)
        {
            Order order = orderStorage.GetByBusinessId(orderBusinessId);
            order.SetProductQuantity(productBusinessId, quantity);
            orderStorage.Update(order);
        }
    }
}