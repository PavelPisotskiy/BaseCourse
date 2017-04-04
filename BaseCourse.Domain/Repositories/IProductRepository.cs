using BaseCourse.Domain.Models.Entities;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Domain.Repositories
{
    public interface IProductRepository : IDependency
    {
        Product GetByBusinessId(string businessId);
        IEnumerable<Product> GetProducts();
        void Create(Product product);
        void Update(Product product);
    }
}