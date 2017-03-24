using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCourse.Application.Models.Dto
{
    public class ProductDto
    {
        public string ProductBusinessId { get; set; }
        public string Name { get; set; }
        public string Units { get; set; }
        public double Price { get; set; }
    }
}
