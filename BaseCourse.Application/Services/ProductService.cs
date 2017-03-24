using BaseCourse.Application.Interfaces;
using BaseCourse.Application.Mappers;
using BaseCourse.Application.Models.Dto;
using BaseCourse.Domain.Models.Entities;
using BaseCourse.Domain.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductStorage productStorage;
        private readonly ProductDtoMapper productDtoMapper;
 
        public ProductService(IProductStorage productStorage)
        {
            this.productStorage = productStorage;
            this.productDtoMapper = new ProductDtoMapper();
        }

        public ProductDto GetProductById(string productBusinessId)
        {
            Product product = productStorage.GetByBusinessId(productBusinessId);
            ProductDto productDto = productDtoMapper.GetProductDto(product);
            return productDto;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            return productStorage.GetProducts().Select(product => productDtoMapper.GetProductDto(product));
        }

        public void Create(ProductDto productDto)
        {
            string productBusinessId = Guid.NewGuid().ToString();
            Product product = new Product(productBusinessId, productDto.Name, productDto.Units);
            product.AddPrice(productDto.Price, DateTime.UtcNow);
            productStorage.Create(product);
        }

        public void AddProductPrice(string productBusinessId, double price, DateTime effectiveDateUtc)
        {
            Product product = productStorage.GetByBusinessId(productBusinessId);
            product.AddPrice(price, effectiveDateUtc);
            productStorage.Update(product);
        }
    }
}