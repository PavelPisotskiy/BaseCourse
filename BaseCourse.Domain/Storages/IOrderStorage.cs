using BaseCourse.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCourse.Domain.Storages
{
    public interface IOrderStorage
    {
        Order GetByBusinessId(string businessId);
        IEnumerable<Order> GetOrders();
        void Create(Order order);
        void Update(Order order);
    }
}
