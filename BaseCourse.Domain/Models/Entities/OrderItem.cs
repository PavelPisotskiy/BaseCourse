using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Domain.Models.Entities
{
    public class OrderItem
    {
        private readonly string productBusinessId;
        public string ProductBusinessId { get { return productBusinessId; } }

        public int Quantity { get; private set; }

        public OrderItem(string productBusinessId, int quantity)
        {
            this.productBusinessId = productBusinessId;
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException("quantity", "Quantity of products must be greater than 0");
            }
            this.Quantity = quantity;
        }
    }
}