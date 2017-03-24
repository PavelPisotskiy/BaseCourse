using BaseCourse.DataSource.Models;
using BaseCourse.Domain.Models.Entities;
using BaseCourse.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource.Mappers
{
    internal class OrderMapper
    {
        private readonly OrderItemMapper orderItemMapper;

        public OrderMapper()
        {
            orderItemMapper = new OrderItemMapper();
        }

        public Order GetOrder(OrderRecord orderRecord, IEnumerable<OrderItemRecord> orderItemRecords)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (OrderItemRecord record in orderItemRecords)
            {
                orderItems.Add(orderItemMapper.GetOrderItem(record));
            }

            return Order.FromPersistence(orderRecord.CustomerId, orderRecord.OrderBusinessId, orderRecord.PlacingDateUtc, (Status)orderRecord.Status, orderItems, orderRecord.TotalPrice);
        }

        public OrderRecord GetOrderRecord(long id, Order order)
        {
            OrderRecord record = new OrderRecord()
            {
                Id = id,
                CustomerId = order.CustomerId,
                OrderBusinessId = order.OrderBusinessId,
                PlacingDateUtc = order.PlacingDateUtc,
                Status = (int)order.Status,
                TotalPrice = order.TotalPrice
            };

            return record;
        }

    }
}