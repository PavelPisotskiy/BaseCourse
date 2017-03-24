using BaseCourse.DataSource.Models;
using BaseCourse.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource.Mappers
{
    internal class OrderItemMapper
    {
        public OrderItem GetOrderItem(OrderItemRecord orderItemRecord)
        {
            return new OrderItem(orderItemRecord.ProductBusinessId, orderItemRecord.Quantity);
        }

        public OrderItemRecord GetOrderItemRecord(long id, string orderBusinessId, OrderItem orderItem)
        {
            OrderItemRecord record = new OrderItemRecord()
            {
                Id = id,
                OrderBusinessId = orderBusinessId,
                ProductBusinessId = orderItem.ProductBusinessId,
                Quantity = orderItem.Quantity
            };

            return record;
        }
    }
}