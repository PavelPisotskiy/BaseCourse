using BaseCourse.DataSource.Interfaces;
using BaseCourse.DataSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource
{
    public class OrderItemRecordsChangeController : IOrderItemRecordsChangeController
    {
        private readonly IEnumerable<OrderItemRecord> existingOrderItemRecords;
        private readonly IEnumerable<OrderItemRecord> newOrderItemRecords;

        public OrderItemRecordsChangeController(IEnumerable<OrderItemRecord> existingOrderItemRecords, IEnumerable<OrderItemRecord> newOrderItemRecords)
        {
            this.existingOrderItemRecords = existingOrderItemRecords;
            this.newOrderItemRecords = newOrderItemRecords;
        }

        public IEnumerable<OrderItemRecord> GetOrderItemRecordsForAdding()
        {
            return newOrderItemRecords
                .Where(orderItemRecord => !existingOrderItemRecords
                    .Any(existingOrderItemRecord => existingOrderItemRecord.ProductBusinessId.Equals(orderItemRecord.ProductBusinessId)))
                .ToList();
        }

        public IEnumerable<OrderItemRecord> GetOrderItemRecordsForDeleting()
        {
            return existingOrderItemRecords
                .Where(existingOrderItemRecord => !newOrderItemRecords
                    .Any(orderItemRecord => orderItemRecord.ProductBusinessId.Equals(existingOrderItemRecord.ProductBusinessId)))
                .ToList();
        }

        public IEnumerable<OrderItemRecord> GetOrderItemRecordsForUpdating()
        {
            return existingOrderItemRecords
                .Where(existingOrderItemRecord => !GetOrderItemRecordsForDeleting()
                    .Any(deletedOrderItem => deletedOrderItem.ProductBusinessId.Equals(existingOrderItemRecord.ProductBusinessId)))
                .ToList();
        }
    }
}