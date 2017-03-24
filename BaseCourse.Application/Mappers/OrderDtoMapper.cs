using BaseCourse.Application.Models.Dto;
using BaseCourse.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Mappers
{
    internal class OrderDtoMapper
    {
        private readonly OrderItemDtoMapper orderItemDtoMapper;

        public OrderDtoMapper()
        {
            orderItemDtoMapper = new OrderItemDtoMapper();
        }

        public OrderDto GetOrderDto(Order order)
        {
            OrderDto orderDto = new OrderDto()
            {
                OrderBusinessId = order.OrderBusinessId,
                OrderItems = order.OrderItems.Select(orderItem => orderItemDtoMapper.GetOrderItemDto(orderItem)),
                PlacingDateUtc = order.PlacingDateUtc,
                Status = (StatusDto)order.Status,
                TotalPrice = order.TotalPrice
            };
            return orderDto;
        }
    }
}