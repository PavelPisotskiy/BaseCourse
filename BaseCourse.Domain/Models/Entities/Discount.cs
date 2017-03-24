using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Domain.Models.Entities
{
    public class Discount
    {
        private double percentage;
        public double Percentage
        {
            get
            {
                return percentage;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Percentage", "Discount should be from 0 to 100 percentage.");
                }

                percentage = value;
            }
        }

        public Discount(double percentage)
        {
            this.Percentage = percentage;
        }

    }
}