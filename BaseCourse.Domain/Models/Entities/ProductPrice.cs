using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Domain.Models.Entities
{
    public class ProductPrice
    {
        private readonly string productBusinessId;
        public string ProductBusinessId { get { return productBusinessId; } }

        public double Price { get; private set; }

        public DateTime EffectiveDateUtc { get; private set; }

        public ProductPrice(string productBusinessId, double price, DateTime effectiveDateUtc)
        {
            this.productBusinessId = productBusinessId;
            this.Price = price;
            this.EffectiveDateUtc = effectiveDateUtc;
        }
    }
}