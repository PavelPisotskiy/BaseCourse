using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource.Models
{
    public class OrderRecord
    {
        public virtual long Id { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual string OrderBusinessId { get; set; }
        public virtual DateTime PlacingDateUtc { get; set; }
        public virtual int Status { get; set; }
        public virtual double TotalPrice { get; set; }
    }
}