using BaseCourse.DataSource.Models;
using BaseCourse.DataSource.Mappers;
using BaseCourse.Domain.Models.Entities;
using BaseCourse.Domain.Repositories;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseCourse.DataSource.Interfaces;

namespace BaseCourse.DataSource.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IRepository<OrderRecord> orderRecordRepository;
        private readonly IRepository<OrderItemRecord> orderItemRecordRepository;
        private readonly OrderMapper orderMapper;
        private readonly OrderItemMapper orderItemMapper;
        private readonly IRepository<ProductRecord> productRecordRepository;

        public OrderRepository(IRepository<OrderRecord> orderRepository, IRepository<OrderItemRecord> orderItemRecordRepository,
            IRepository<ProductRecord> productRecordRepository)
        {
            this.orderRecordRepository = orderRepository;
            this.orderItemRecordRepository = orderItemRecordRepository;
            this.productRecordRepository = productRecordRepository;
            this.orderMapper = new OrderMapper();
            this.orderItemMapper = new OrderItemMapper();
        }

        public Order GetByBusinessId(string businessId)
        {
            if (string.IsNullOrEmpty(businessId))
            {
                throw new ArgumentNullException("businessId");
            }

            OrderRecord orderRecord = orderRecordRepository.Table.FirstOrDefault(o => o.OrderBusinessId.Equals(businessId));
            if (orderRecord == null)
            {
                return null;
            }
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
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }
            OrderRecord orderRecord = orderMapper.GetOrderRecord(0, order);
            orderRecordRepository.Create(orderRecord);
        }

        public void Update(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            OrderRecord orderRecord = orderRecordRepository.Table.FirstOrDefault(o => o.OrderBusinessId.Equals(order.OrderBusinessId));
            if (orderRecord == null)
            {
                throw new ArgumentException("Order is not found.");
            }
            orderRecord.PlacingDateUtc = order.PlacingDateUtc;
            orderRecord.Status = (int)order.Status;
            orderRecord.TotalPrice = order.TotalPrice;
            orderRecordRepository.Update(orderRecord);
            CheckOrderItems(orderRecord.Id, order.OrderBusinessId, order.OrderItems);
        }

        private void CheckOrderItems(long orderId, string orderBusinessId, IEnumerable<OrderItem> newOrderItems)
        {
            IEnumerable<OrderItemRecord> existingOrderItemRecords = orderItemRecordRepository.Table
                .Where(oi => oi.OrderBusinessId.Equals(orderBusinessId))
                .ToList();

            IEnumerable<OrderItemRecord> newOrderItemRecords = newOrderItems.Select(orderItem =>
            {
                long productId = productRecordRepository.Table.FirstOrDefault(p => p.ProductBusinessId.Equals(orderItem.ProductBusinessId)).Id;
                return orderItemMapper.GetOrderItemRecord(0, orderId, productId, orderItem);
            }).ToList();

            //var changeController = new OrderItemRecordsChangeController(existingOrderItemRecords, newOrderItemRecords);

            //IEnumerable<OrderItemRecord> orderItemRecordsForAdding = changeController.GetOrderItemRecordsForAdding().ToList();
            //IEnumerable<OrderItemRecord> orderItemRecordsForDeleting = changeController.GetOrderItemRecordsForDeleting().ToList();
            //IEnumerable<OrderItemRecord> orderItemRecordsForUpdating = changeController.GetOrderItemRecordsForUpdating().ToList();

            //AddNewOrderItemRecords(orderBusinessId, orderItemRecordsForAdding);
            //DeleteOrderItemRecords(orderItemRecordsForDeleting);
            //UpdateOrderItemRecords(orderItemRecordsForUpdating);

            DeleteOrderItemRecords(existingOrderItemRecords);
            AddNewOrderItemRecords(orderBusinessId, newOrderItemRecords);
            
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