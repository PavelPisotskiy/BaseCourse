using BaseCourse.DataSource.Models;
using BaseCourse.DataSource.Mappers;
using BaseCourse.Domain.Models.Entities;
using BaseCourse.Domain.Storages;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseCourse.DataSource.Interfaces;

namespace BaseCourse.DataSource.Storages
{
    public class OrderStorage : IOrderStorage
    {
        private readonly IRepository<OrderRecord> orderRecordRepository;
        private readonly IRepository<OrderItemRecord> orderItemRecordRepository;
        private readonly OrderMapper orderMapper;
        private readonly OrderItemMapper orderItemMapper;

        public OrderStorage(IRepository<OrderRecord> orderRepository, IRepository<OrderItemRecord> orderItemRecordRepository)
        {
            this.orderRecordRepository = orderRepository;
            this.orderItemRecordRepository = orderItemRecordRepository;
            this.orderMapper = new OrderMapper();
            this.orderItemMapper = new OrderItemMapper();
        }

        public Order GetByBusinessId(string businessId)
        {
            OrderRecord orderRecord = orderRecordRepository.Table.FirstOrDefault(o => o.OrderBusinessId.Equals(businessId));
            IEnumerable<OrderItemRecord> orderItemRecords = orderItemRecordRepository.Table.Where(oi => oi.OrderBusinessId.Equals(businessId));
            Order order = orderMapper.GetOrder(orderRecord, orderItemRecords);
            return order;
        }

        public IEnumerable<Order> GetOrders()
        {
            IEnumerable<OrderRecord> orderRecords = orderRecordRepository.Table;
            List<Order> orders = new List<Order>();
            foreach (OrderRecord orderRecord in orderRecords)
            {
                IEnumerable<OrderItemRecord> orderItemRecords = orderItemRecordRepository.Table.Where(oi => oi.OrderBusinessId.Equals(orderRecord.OrderBusinessId));
                Order order = orderMapper.GetOrder(orderRecord, orderItemRecords);
                orders.Add(order);
            }
            return orders;
        }

        public void Create(Order order)
        {
            OrderRecord orderRecord = orderMapper.GetOrderRecord(0, order);
            orderRecordRepository.Create(orderRecord);
        }

        public void Update(Order order)
        {
            OrderRecord orderRecord = orderRecordRepository.Table.FirstOrDefault(o => o.OrderBusinessId.Equals(order.OrderBusinessId));
            orderRecord.PlacingDateUtc = order.PlacingDateUtc;
            orderRecord.Status = (int)order.Status;
            orderRecord.TotalPrice = order.TotalPrice;
            orderRecordRepository.Update(orderRecord);
            CheckOrderItems(order.OrderBusinessId, order.OrderItems);
        }

        private void CheckOrderItems(string orderBusinessId, IEnumerable<OrderItem> newOrderItems)
        {
            IEnumerable<OrderItemRecord> existingOrderItemRecords = orderItemRecordRepository.Table
                .Where(oi => oi.OrderBusinessId.Equals(orderBusinessId))
                .ToList();

            IEnumerable<OrderItemRecord> newOrderItemRecords = newOrderItems.Select(orderItem => orderItemMapper.GetOrderItemRecord(0, orderBusinessId, orderItem));

            var changeController = new OrderItemRecordsChangeController(existingOrderItemRecords, newOrderItemRecords);

            IEnumerable<OrderItemRecord> orderItemRecordsForAdding = changeController.GetOrderItemRecordsForAdding();
            IEnumerable<OrderItemRecord> orderItemRecordsForDeleting = changeController.GetOrderItemRecordsForDeleting();
            IEnumerable<OrderItemRecord> orderItemRecordsForUpdating = changeController.GetOrderItemRecordsForUpdating();

            AddNewOrderItemRecords(orderBusinessId, orderItemRecordsForAdding);
            DeleteOrderItemRecords(orderItemRecordsForDeleting);
            UpdateOrderItemRecords(orderItemRecordsForUpdating);
        }

        private void UpdateOrderItemRecords(IEnumerable<OrderItemRecord> orderItemRecordForUpdating)
        {
            foreach (OrderItemRecord orderItemRecord in orderItemRecordForUpdating)
            {
                orderItemRecordRepository.Update(orderItemRecord);
            }
        }

        private void DeleteOrderItemRecords(IEnumerable<OrderItemRecord> orderItemRecordForDeleting)
        {
            foreach (OrderItemRecord orderItemRecord in orderItemRecordForDeleting)
            {
                orderItemRecordRepository.Delete(orderItemRecord);
            }
        }

        private void AddNewOrderItemRecords(string orderBusinessId, IEnumerable<OrderItemRecord> orderItemRecordsForAdding)
        {
            foreach (OrderItemRecord orderItemRecord in orderItemRecordsForAdding)
            {
                orderItemRecordRepository.Create(orderItemRecord);
            }
        }


    }
}