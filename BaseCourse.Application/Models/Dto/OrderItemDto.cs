using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCourse.Application.Models.Dto
{
    public class OrderItemDto
    {
        public string ProductBusinessId { get; set; }
        public int Quantity { get; set; }
    }
}
