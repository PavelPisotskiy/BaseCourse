using BaseCourse.Domain.Models.Entities;
using BaseCourse.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseCourse.Domain.Repositories;

namespace BaseCourse.Domain
{
    public class PriceCalculator : IPriceCalculator
    {
        private Discount discount;
        private IProductRepository productStorage;

        public PriceCalculator(IProductRepository productStorage, Discount discount)
        {
            this.discount = discount;
            this.productStorage = productStorage;
        }

        public PriceCalculator(IProductRepository productStorage)
            : this(productStorage, null)
        {

        }

        public double Calculate(Order order)
        {
            double sum = 0;

            foreach (var orderItem in order.OrderItems)
            {
                Product product = productStorage.GetByBusinessId(orderItem.ProductBusinessId);
                sum += orderItem.Quantity * product.ActualPrice().Price;
            }

            if (discount == null)
            {
                return sum;
            }
            else
            {
                return sum * (1 - discount.Percentage / 100);
            }
        }
    }
}