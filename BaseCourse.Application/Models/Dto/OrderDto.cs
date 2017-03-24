using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Models.Dto
{
    public class OrderDto
    {
        public string OrderBusinessId { get; set; }
        public DateTime PlacingDateUtc { get; set; }
        public StatusDto Status { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public double TotalPrice { get; set; }
    }
}