using BaseCourse.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCourse.Domain.Interfaces
{
    public interface IPriceCalculator
    {
        double Calculate(Order order);
    }
}
