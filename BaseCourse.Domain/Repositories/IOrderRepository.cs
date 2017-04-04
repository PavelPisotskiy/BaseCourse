using BaseCourse.Domain.Models.Entities;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCourse.Domain.Repositories
{
    public interface IOrderRepository : IDependency
    {
        Order GetByBusinessId(string businessId);
        IEnumerable<Order> GetOrders();
        void Create(Order order);
        void Update(Order order);
    }
}
