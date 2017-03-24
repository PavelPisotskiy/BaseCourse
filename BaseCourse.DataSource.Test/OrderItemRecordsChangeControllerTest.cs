using BaseCourse.DataSource.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource.Test
{
    [TestFixture]
    public class OrderItemRecordsChangeControllerTest
    {
        private IEnumerable<OrderItemRecord> existingOrderItemRecords;
        private string orderBusinessId;

        [SetUp]
        public void TestInit()
        {
            orderBusinessId = Guid.NewGuid().ToString();

            existingOrderItemRecords = new List<OrderItemRecord>()
            {
                new OrderItemRecord() { Id = 1, OrderBusinessId = orderBusinessId, ProductBusinessId = "ProductId_1", Quantity = 1 },
                new OrderItemRecord() { Id = 2, OrderBusinessId = orderBusinessId, ProductBusinessId = "ProductId_2", Quantity = 1 }
            };
        }

        [Test]
        public void GetOrderItemRecordsForAdding_Must_Return_Id_Of_Added_Record()
        {
            
            var newOrderItemRecords = new List<OrderItemRecord>()
            {
                new OrderItemRecord() { Id = 1, OrderBusinessId = orderBusinessId, ProductBusinessId = "ProductId_1", Quantity = 1 },
                new OrderItemRecord() { Id = 2, OrderBusinessId = orderBusinessId, ProductBusinessId = "ProductId_2", Quantity = 1 },
                new OrderItemRecord() { Id = 3, OrderBusinessId = orderBusinessId, ProductBusinessId = "ProductId_3", Quantity = 1 }
            };

            var changeController = new OrderItemRecordsChangeController(existingOrderItemRecords, newOrderItemRecords);

            var recordsForAdding = changeController.GetOrderItemRecordsForAdding();

            foreach (var record in recordsForAdding)
            {
                Assert.AreEqual(3, record.Id);
            }
        }

        [Test]
        public void GetOrderItemRecordsForDeleting_Must_Return_Id_Of_Deleted_Record()
        {

            var newOrderItemRecords = new List<OrderItemRecord>()
            {
                new OrderItemRecord() { Id = 1, OrderBusinessId = orderBusinessId, ProductBusinessId = "ProductId_1", Quantity = 1 }
            };

            var changeController = new OrderItemRecordsChangeController(existingOrderItemRecords, newOrderItemRecords);

            var recordsForDeleting = changeController.GetOrderItemRecordsForDeleting();

            foreach (var record in recordsForDeleting)
            {
                Assert.AreEqual(2, record.Id);
            }
        }

        [Test]
        public void GetOrderItemRecordsForUpdating_Must_Return_Id_Of_Records_For_Updating_If_Add_New_Record()
        {

            var newOrderItemRecords = new List<OrderItemRecord>()
            {
                new OrderItemRecord() { Id = 1, OrderBusinessId = orderBusinessId, ProductBusinessId = "ProductId_1", Quantity = 1 },
                new OrderItemRecord() { Id = 2, OrderBusinessId = orderBusinessId, ProductBusinessId = "ProductId_2", Quantity = 1 },
                new OrderItemRecord() { Id = 3, OrderBusinessId = orderBusinessId, ProductBusinessId = "ProductId_3", Quantity = 1 }
            };

            var changeController = new OrderItemRecordsChangeController(existingOrderItemRecords, newOrderItemRecords);

            var recordsForUpdating = changeController.GetOrderItemRecordsForUpdating();

            foreach (var record in recordsForUpdating)
            {
                Assert.AreNotEqual(3, record.Id);
            }
        }
    }
}