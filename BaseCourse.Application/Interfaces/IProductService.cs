using BaseCourse.Application.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Interfaces
{
    public interface IProductService
    {
        ProductDto GetProductById(string productBusinessId);
        IEnumerable<ProductDto> GetProducts();
        void Create(ProductDto product);
        void AddProductPrice(string productBusinessId, double price, DateTime effectiveDateUtc);
    }
}