using BaseCourse.Application.Models.Dto;
using BaseCourse.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Mappers
{
    public class ProductDtoMapper
    {
        public ProductDto GetProductDto(Product product)
        {
            ProductDto productDto = new ProductDto()
            {
                ProductBusinessId = product.ProductBusinessId,
                Name = product.Name,
                Units = product.Units,
                Price = product.ActualPrice().Price
            };
            return productDto;
        }
    }
}