using BaseCourse.Domain.Models.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Domain.Test
{
    [TestFixture]
    public class ProductTest
    {
        [Test]
        public void ActualPrice_Must_Return_Effective_Date_Of_Price()
        {
            string productBusinessId = Guid.NewGuid().ToString();

            DateTime expectedDate = DateTime.UtcNow;
            DateTime nextDay = expectedDate.AddDays(1);
            DateTime previousDay = expectedDate.AddDays(-1);
            List<ProductPrice> prices = new List<ProductPrice>()
            { 
                new ProductPrice(productBusinessId, 100, nextDay),
                new ProductPrice(productBusinessId, 250, expectedDate),
                new ProductPrice(productBusinessId, 700, previousDay)
            };

            Product product = new Product(productBusinessId, "Test Product", "kg", prices);

            Assert.AreEqual(expectedDate, product.ActualPrice().EffectiveDateUtc);
        }

        [Test]
        public void AddPrice_Must_Throw_Exception_If_Change_Price_Of_Product_In_Past()
        {
            string productBusinessId = Guid.NewGuid().ToString();

            DateTime currentDate = DateTime.UtcNow;
            List<ProductPrice> prices = new List<ProductPrice>()
            { 
                new ProductPrice(productBusinessId, 250, currentDate)
            };

            Product product = new Product(productBusinessId, "Test Product", "kg", prices);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => product.AddPrice(100, currentDate.AddSeconds(-1)));
            Assert.That(ex.Message, Is.EqualTo("You can't set price in the past.\r\nParameter name: effectiveDateUtc"));
        }

        [Test]
        public void AddPrice_Must_Throw_Exception_If_Price_Less_Than_0()
        {
            string productBusinessId = Guid.NewGuid().ToString();

            DateTime currentDate = DateTime.UtcNow;
            List<ProductPrice> prices = new List<ProductPrice>()
            { 
                new ProductPrice(productBusinessId, 250, currentDate)
            };

            Product product = new Product(productBusinessId, "Test Product", "kg", prices);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => product.AddPrice(0, currentDate.AddHours(1)));
            Assert.That(ex.Message, Is.EqualTo("Price of products must be greater than 0.\r\nParameter name: price"));
        }
    }
}