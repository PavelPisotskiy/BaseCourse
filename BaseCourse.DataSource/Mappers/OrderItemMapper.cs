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
            return new OrderItem(orderItemRecord.OrderBusinessId, orderItemRecord.ProductBusinessId, orderItemRecord.Quantity);
        }

        public OrderItemRecord GetOrderItemRecord(long id, long orderId, long productId, OrderItem orderItem)
        {
            OrderItemRecord record = new OrderItemRecord()
            {
                Id = id,
                OrderId = orderId,
                ProductId = productId,
                OrderBusinessId = orderItem.OrderBusinessId,
                ProductBusinessId = orderItem.ProductBusinessId,
                Quantity = orderItem.Quantity
            };

            return record;
        }
    }
}