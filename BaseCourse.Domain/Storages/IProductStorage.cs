using BaseCourse.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Domain.Storages
{
    public interface IProductStorage
    {
        Product GetByBusinessId(string businessId);
        IEnumerable<Product> GetProducts();
        void Create(Product product);
        void Update(Product product);
    }
}