using BaseCourse.Application.Interfaces;
using BaseCourse.Application.Mappers;
using BaseCourse.Application.Models.Dto;
using BaseCourse.Application.Permissions;
using BaseCourse.Domain;
using BaseCourse.Domain.Interfaces;
using BaseCourse.Domain.Models.Entities;
using BaseCourse.Domain.Models.Enums;
using BaseCourse.Domain.Repositories;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseCourse.Application.Exceptions;

namespace BaseCourse.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly OrderDtoMapper orderDtoMapper;
        private readonly IUserService userService;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository,
            IUserService userService)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.userService = userService;
            orderDtoMapper = new OrderDtoMapper();
        }

        public OrderDto GetCart()
        {
            Order currentOrder = GetCartOfCurrentUser();
            OrderDto orderDto = orderDtoMapper.GetOrderDto(currentOrder);
            return orderDto;
        }

        public IEnumerable<OrderDto> GetCurrentCustomerOrders()
        {
            IEnumerable<OrderDto> orders = null;
            if (userService.VerifyUserPermission(PermissionProvider.GetOrdersOfCurrentCustomer))
            {
                int customerId = userService.GetCurrentUser().Id;
                orders = orderRepository.GetOrders().Where(order => order.CustomerId == customerId).Select(order => orderDtoMapper.GetOrderDto(order));
            }
            else
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }

            return orders;
        }

        public OrderDto Get(string orderId)
        {
            Order order = orderRepository.GetByBusinessId(orderId);
            if (order == null)
            {
                throw new OrderNotFoundException("Order is not found.");
            }

            if (userService.VerifyUserPermission(PermissionProvider.GetOrdersOfCurrentCustomer))
            {
                if (order.CustomerId != userService.GetCurrentUser().Id)
                {
                    throw new OrderNotFoundException("Order is not found.");
                }
            }
            else if (!userService.VerifyUserPermission(PermissionProvider.GetAllOrders))
            {
                throw new PermissionException("You do not have permission to perform this action."); 
            }
            OrderDto orderDto = orderDtoMapper.GetOrderDto(order);
            return orderDto;
        }
        
        public IEnumerable<OrderDto> GetAllProcessingOrders()
        {
            IEnumerable<OrderDto> orders = null;
            if (userService.VerifyUserPermission(PermissionProvider.GetAllOrders))
            {
                orders = orderRepository.GetOrders().Where(order => order.Status == Status.Processing).Select(order => orderDtoMapper.GetOrderDto(order));
            }
            else
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }

            return orders;
        }

        public void AddToCart(string productId)
        {
            if (!userService.VerifyUserPermission(PermissionProvider.ModifyOrderOfCurrentCustomer))
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }

            Order order = GetCartOfCurrentUser();

            Product product = productRepository.GetByBusinessId(productId);
            if (product == null)
            {
                throw new ProductNotFoundException("Product is not found.");
            }
            order.AddProduct(productId, 1);
            orderRepository.Update(order);
        }

        public void RemoveFromCart(string productId)
        {
            if (!userService.VerifyUserPermission(PermissionProvider.ModifyOrderOfCurrentCustomer))
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }

            Order order = GetCartOfCurrentUser();

            order.RemoveProduct(productId);
            orderRepository.Update(order);
        }

        public void SetProductQuantity(string productBusinessId, int quantity)
        {
            if (!userService.VerifyUserPermission(PermissionProvider.ModifyOrderOfCurrentCustomer))
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }

            Order order = GetCartOfCurrentUser();

            order.SetProductQuantity(productBusinessId, quantity);
            orderRepository.Update(order);
        }
                
        public void Checkout(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentNullException("orderId");
            }

            Order order = orderRepository.GetByBusinessId(orderId);
            if (order == null)
            {
                throw new OrderNotFoundException("Order is not found.");
            }

            if (userService.VerifyUserPermission(PermissionProvider.CheckoutOrderOfCurrentCustomer))
            {
                if (order.CustomerId != userService.GetCurrentUser().Id)
                {
                    throw new OrderNotFoundException("Order is not found.");
                }
            }
            else
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }

            IPriceCalculator calculator = new PriceCalculator(productRepository);
            order.Checkout(calculator);
            orderRepository.Update(order);
        }

        public void Accept(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentNullException("orderId");
            }

            Order order = orderRepository.GetByBusinessId(orderId);
            if (order == null)
            {
                throw new OrderNotFoundException("Order is not found.");
            }

            if (!userService.VerifyUserPermission(PermissionProvider.AcceptOrderOfAnyCustomer))
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }

            order.Accept();
            orderRepository.Update(order);
        }

        public void Reject(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentNullException("orderId");
            }

            Order order = orderRepository.GetByBusinessId(orderId);
            if (order == null)
            {
                throw new OrderNotFoundException("Order is not found.");
            }

            if (userService.VerifyUserPermission(PermissionProvider.RejectOrderOfAnyCustomer))
            {
                order.Reject();
                orderRepository.Update(order);
            }
            else if (userService.VerifyUserPermission(PermissionProvider.RejectOrderOfCurrentCustomer))
            {
                if (order.CustomerId != userService.GetCurrentUser().Id)
                {
                    throw new OrderNotFoundException("Order is not found.");
                }

                order.Reject();
                orderRepository.Update(order);
            }
            else
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }
        }

        private Order GetCartOfCurrentUser()
        {
            if (!userService.VerifyUserPermission(PermissionProvider.GetOrdersOfCurrentCustomer) ||
                !userService.VerifyUserPermission(PermissionProvider.CreateOrderForCurrentCustomer))
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }


            int currentCustomerId = userService.GetCurrentUser().Id;
            Order currentOrder = orderRepository.GetOrders().FirstOrDefault(order => order.CustomerId == currentCustomerId && order.Status == Status.Open);
            if (currentOrder == null)
            {
                string orderBusinessId = Guid.NewGuid().ToString();
                currentOrder = new Order(currentCustomerId, orderBusinessId);
                orderRepository.Create(currentOrder);
            }

            return currentOrder;
        }
    }
}