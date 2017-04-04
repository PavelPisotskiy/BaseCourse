using BaseCourse.Domain.Models;
using BaseCourse.Domain.Models.Entities;
using BaseCourse.Domain.Models.Enums;
using BaseCourse.Domain.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moq;
using BaseCourse.Domain.Repositories;

namespace BaseCourse.Domain.Test
{
    [TestFixture]
    public class OrderTest
    {
        public class Checkout
        {
        }
        [Test]
        public void TotalPrice_Must_Return_Calculated_Price_Of_Three_Products()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();

            string productBusinessId1 = Guid.NewGuid().ToString();
            string productBusinessId2 = Guid.NewGuid().ToString();
            string productBusinessId3 = Guid.NewGuid().ToString();

            double productPrice1 = 200;
            double productPrice2 = 29;
            double productPrice3 = 800;

            int productQuantity1 = 2;
            int productQuantity2 = 4;
            int productQuantity3 = 8;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId1, productPrice1, DateTime.UtcNow.AddHours(-1)),
                new ProductPrice(productBusinessId2, productPrice2, DateTime.UtcNow.AddHours(-1)),
                new ProductPrice(productBusinessId3, productPrice3, DateTime.UtcNow.AddHours(-1))
            };


            Product testProduct1 = new Product(productBusinessId1, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId1));
            Product testProduct2 = new Product(productBusinessId2, "Test Product 2", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId2));
            Product testProduct3 = new Product(productBusinessId3, "Test Product 3", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId3));

            var products = new List<Product>()
            {
                testProduct1,
                testProduct2,
                testProduct3
            };

            var mock = new Mock<IProductRepository>();
            mock.Setup(s => s.GetByBusinessId(It.IsAny<string>())).Returns<string>((id) => products.FirstOrDefault(p => p.ProductBusinessId == id));

            IPriceCalculator calculator = new PriceCalculator(mock.Object);
            Order order = new Order(customerId, orderBusinessId);

            order.AddProduct(productBusinessId1, productQuantity1);
            order.AddProduct(productBusinessId2, productQuantity2);
            order.AddProduct(productBusinessId3, productQuantity3);

            double expected = (productQuantity1 * productPrice1) + (productQuantity2 * productPrice2) + (productQuantity3 * productPrice3);
            order.Checkout(calculator);
            Assert.AreEqual(expected, order.TotalPrice);
        }

        [Test]
        public void AddProduct_Must_Return_One_OrderItem_If_Add_Twice_One_Product()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Order order = new Order(customerId, orderBusinessId);
            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            order.AddProduct(productBusinessId, productQuantity);
            order.AddProduct(productBusinessId, productQuantity);

            Assert.AreEqual(1, order.OrderItems.Count());
        }

        [Test]
        public void AddProduct_Must_Return_Sum_Of_Quantities_Of_Product_If_Add_Twice_One_Product()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Order order = new Order(customerId, orderBusinessId);
            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            order.AddProduct(productBusinessId, productQuantity);
            order.AddProduct(productBusinessId, productQuantity);

            int expectedQuantity = 2;

            Assert.AreEqual(expectedQuantity, order.OrderItems.First().Quantity);
        }

        [Test]
        public void Checkout_Must_Return_Processing_Status()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            var mock = new Mock<IProductRepository>();
            mock.Setup(s => s.GetByBusinessId(productBusinessId)).Returns(testProduct1);

            IPriceCalculator calculator = new PriceCalculator(mock.Object);
            Order order = new Order(customerId, orderBusinessId);
            order.AddProduct(productBusinessId, productQuantity);

            Status expectedStatus = Status.Processing;
            order.Checkout(calculator);
            Assert.AreEqual(expectedStatus, order.Status);
        }

        [Test]
        public void Accept_Must_Return_Accept_Status()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            var mock = new Mock<IProductRepository>();
            mock.Setup(s => s.GetByBusinessId(productBusinessId)).Returns(testProduct1);

            IPriceCalculator calculator = new PriceCalculator(mock.Object);
            Order order = new Order(customerId, orderBusinessId);
            order.AddProduct(productBusinessId, productQuantity);

            Status expectedStatus = Status.Accepted;
            order.Checkout(calculator);
            order.Accept();
            Assert.AreEqual(expectedStatus, order.Status);
        }

        [Test]
        public void Reject_Must_Return_Rejected_Status()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            var mock = new Mock<IProductRepository>();
            mock.Setup(s => s.GetByBusinessId(productBusinessId)).Returns(testProduct1);

            IPriceCalculator calculator = new PriceCalculator(mock.Object);
            Order order = new Order(customerId, orderBusinessId);
            order.AddProduct(productBusinessId, productQuantity);

            Status expectedStatus = Status.Rejected;
            order.Checkout(calculator);
            order.Reject();
            Assert.AreEqual(expectedStatus, order.Status);
        }

        [Test]
        public void Accept_Must_Throw_Exception_If_Cancel_Order_In_Rejected_Status()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            var mock = new Mock<IProductRepository>();
            mock.Setup(s => s.GetByBusinessId(productBusinessId)).Returns(testProduct1);

            IPriceCalculator calculator = new PriceCalculator(mock.Object);
            Order order = new Order(customerId, orderBusinessId);
            order.AddProduct(productBusinessId, productQuantity);
            order.Checkout(calculator);
            order.Reject();

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => order.Accept());
            Assert.That(ex.Message, Is.EqualTo("You can accept order only in 'Processing' status"));
        }

        [Test]
        public void Reject_Must_Throw_Exception_If_Cancel_Order_In_Accepted_Status()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            var mock = new Mock<IProductRepository>();
            mock.Setup(s => s.GetByBusinessId(productBusinessId)).Returns(testProduct1);

            IPriceCalculator calculator = new PriceCalculator(mock.Object);
            Order order = new Order(customerId, orderBusinessId);
            order.AddProduct(productBusinessId, productQuantity);
            order.Checkout(calculator);
            order.Accept();

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => order.Reject());
            Assert.That(ex.Message, Is.EqualTo("You can reject order only in 'Processing' status"));
        }

        [Test]
        public void Checkout_Must_Return_TotalPrice_With_Discount()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;
            double discountInPercentage = 30;

            double expectedPrice = (productPrice * productQuantity) * (1 - discountInPercentage / 100);

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            var mock = new Mock<IProductRepository>();
            mock.Setup(s => s.GetByBusinessId(productBusinessId)).Returns(testProduct1);

            Discount discount = new Discount(discountInPercentage);

            IPriceCalculator calculator = new PriceCalculator(mock.Object, discount);
            Order order = new Order(customerId, orderBusinessId);
            order.AddProduct(productBusinessId, productQuantity);

            order.Checkout(calculator);

            Assert.AreEqual(expectedPrice, order.TotalPrice);
        }

        [Test]
        public void Percentage_Must_Throw_Exception_If_Set_Negative_Discount()
        {
            double discountInPercentage = 30;

            Discount discount = new Discount(discountInPercentage);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => discount.Percentage = -1);
            Assert.That(ex.Message, Is.EqualTo("Discount should be from 0 to 100 percentage.\r\nParameter name: Percentage"));
        }

        [Test]
        public void Percentage_Must_Throw_Exception_If_Set_Discount_More_Than_100_Percentage()
        {
            double discountInPercentage = 30;

            Discount discount = new Discount(discountInPercentage);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => discount.Percentage = 101);
            Assert.That(ex.Message, Is.EqualTo("Discount should be from 0 to 100 percentage.\r\nParameter name: Percentage"));
        }

        [Test]
        public void Checkout_Must_Throw_Exception_If_Order_Is_Empty()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            Order order = new Order(customerId, orderBusinessId);
            var mock = new Mock<IProductRepository>();
            IPriceCalculator calculator = new PriceCalculator(mock.Object);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => order.Checkout(calculator));
            Assert.That(ex.Message, Is.EqualTo("You order is empty."));   
        }

        [Test]
        public void RemoveProduct_Must_Return_Null()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Order order = new Order(customerId, orderBusinessId);
            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            order.AddProduct(productBusinessId, productQuantity);
            order.RemoveProduct(productBusinessId);

            OrderItem orderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductBusinessId == productBusinessId);

            Assert.AreEqual(null, orderItem);
        }

        [Test]
        public void SetProductQuantity_Must_Return_Changed_Product_Quantity()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;
            int changedProductQuantity = 5;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Order order = new Order(customerId, orderBusinessId);
            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            order.AddProduct(productBusinessId, productQuantity);
            order.SetProductQuantity(productBusinessId, changedProductQuantity);

            OrderItem orderItem = order.OrderItems.FirstOrDefault(_orderItem => _orderItem.ProductBusinessId.Equals(productBusinessId));

            Assert.AreEqual(changedProductQuantity, orderItem.Quantity);
        }

        [Test]
        public void SetProductQuantity_Must_Throw_Exception_If_Try_To_Change_Product_Quantity_In_Processing_Status()
        {
            int customerId = 1;
            string orderBusinessId = Guid.NewGuid().ToString();
            string productBusinessId = Guid.NewGuid().ToString();
            double productPrice = 200;
            int productQuantity = 1;
            int changedProductQuantity = 5;

            List<ProductPrice> prices = new List<ProductPrice>()
            {
                new ProductPrice(productBusinessId, productPrice, DateTime.UtcNow.AddHours(-1)),
            };

            Order order = new Order(customerId, orderBusinessId);
            Product testProduct1 = new Product(productBusinessId, "Test Product 1", "kg", prices.Where(p => p.ProductBusinessId == productBusinessId));
            order.AddProduct(productBusinessId, productQuantity);

            var mock = new Mock<IProductRepository>();
            mock.Setup(s => s.GetByBusinessId(productBusinessId)).Returns(testProduct1);
            IPriceCalculator calculator = new PriceCalculator(mock.Object);
            order.Checkout(calculator);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => order.SetProductQuantity(productBusinessId, changedProductQuantity));
            Assert.That(ex.Message, Is.EqualTo("You can change product quantity only in 'Open' status"));   
        }
    }
}