using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource.Models
{
    public class OrderItemRecord
    {
        public virtual long Id { get; set; }
        public virtual string OrderBusinessId { get; set; }
        public virtual string ProductBusinessId { get; set; }
        public virtual int Quantity { get; set; }
    }
}