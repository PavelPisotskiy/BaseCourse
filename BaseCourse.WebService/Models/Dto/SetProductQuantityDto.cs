using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseCourse.WebService.Models.Dto
{
    public class SetProductQuantityDto
    {
        [Required]
        [StringLength(36)]
        public string ProductBusinessId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}