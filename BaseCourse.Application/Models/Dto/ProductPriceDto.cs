using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Models.Dto
{
    public class ProductPriceDto
    {
        public string ProductBusinessId { get; set; }
        public double Price { get; set; }
        public DateTime EffectiveDateUtc { get; set; }
    }
}