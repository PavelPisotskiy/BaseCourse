using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource.Models
{
    public class ProductPriceRecord
    {
        public virtual long Id { get; set; }
        public virtual long ProductId { get; set; }
        public virtual string ProductBusinessId { get; set; }
        public virtual double Price { get; set; }
        public virtual DateTime EffectiveDateUtc { get; set; }
    }
}