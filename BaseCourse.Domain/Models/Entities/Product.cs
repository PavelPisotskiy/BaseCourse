using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Domain.Models.Entities
{
    public class Product
    {
        private readonly string productBusinessId;
        public string ProductBusinessId { get { return productBusinessId; } }

        public string Name { get; private set; }

        private readonly string units;
        public string Units { get { return units; } }

        public IEnumerable<ProductPrice> Prices { get { return prices; } }

        private readonly List<ProductPrice> prices = new List<ProductPrice>();

        public ProductPrice ActualPrice()
        {
            return Prices.Where(p => p.EffectiveDateUtc <= DateTime.UtcNow)
                            .OrderBy(p => p.EffectiveDateUtc)
                            .Last(); 
        }

        public Product(string productBusinessId, string name, string units, IEnumerable<ProductPrice> prices)
        {
            this.productBusinessId = productBusinessId;
            this.Name = name;
            this.units = units;
            this.prices.AddRange(prices);
        }

        public Product(string productBusinessId, string name, string units)
            : this(productBusinessId, name, units, Enumerable.Empty<ProductPrice>())
        {
        }

        public void AddPrice(double price, DateTime effectiveDateUtc)
        {
            if (effectiveDateUtc < DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException("effectiveDateUtc", "You can't set price in the past.");
            }

            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException("price", "Price of products must be greater than 0.");
            }

            ProductPrice productPrice = new ProductPrice(ProductBusinessId, price, effectiveDateUtc);
            prices.Add(productPrice);
        }
    }
}