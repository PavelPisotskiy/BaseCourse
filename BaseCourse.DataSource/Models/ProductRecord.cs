using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource.Models
{
    public class ProductRecord
    {
        public virtual long Id { get; set; }
        public virtual string ProductBusinessId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Units { get; set; }
    }
}