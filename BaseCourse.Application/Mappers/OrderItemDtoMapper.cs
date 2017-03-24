using BaseCourse.Application.Models.Dto;
using BaseCourse.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Mappers
{
    internal class OrderItemDtoMapper
    {
        public OrderItemDto GetOrderItemDto(OrderItem orderItem)
        {
            OrderItemDto orderItemDto = new OrderItemDto()
            {
                ProductBusinessId = orderItem.ProductBusinessId,
                Quantity = orderItem.Quantity
            };
            return orderItemDto;
        }
    }
}