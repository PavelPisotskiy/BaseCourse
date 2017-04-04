using BaseCourse.Application.Exceptions;
using BaseCourse.Application.Interfaces;
using BaseCourse.Application.Mappers;
using BaseCourse.Application.Models.Dto;
using BaseCourse.Application.Permissions;
using BaseCourse.Domain.Models.Entities;
using BaseCourse.Domain.Repositories;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ProductDtoMapper productDtoMapper;
        private readonly IUserService userService;

        public ProductService(IProductRepository productStorage, IUserService userService)
        {
            this.productRepository = productStorage;
            this.productDtoMapper = new ProductDtoMapper();
            this.userService = userService;
        }

        public ProductDto GetProductById(string productBusinessId)
        {
            if (!userService.VerifyUserPermission(PermissionProvider.GetProducts))
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }

            Product product = productRepository.GetByBusinessId(productBusinessId);
            ProductDto productDto = productDtoMapper.GetProductDto(product);
            return productDto;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (!userService.VerifyUserPermission(PermissionProvider.GetProducts))
            {
                throw new PermissionException("You do not have permission to perform this action.");  
            }

            return productRepository.GetProducts().Select(product => productDtoMapper.GetProductDto(product));
        }

        public void Create(ProductDto productDto)
        {
            if (!userService.VerifyUserPermission(PermissionProvider.CreateProducts))
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }

            Product product = new Product(productDto.ProductBusinessId, productDto.Name, productDto.Units);
            product.AddPrice(productDto.Price, DateTime.UtcNow);
            productRepository.Create(product);
        }

        public void AddProductPrice(ProductPriceDto productPrice)
        {
            if (!userService.VerifyUserPermission(PermissionProvider.AddProductPrice))
            {
                throw new PermissionException("You do not have permission to perform this action.");
            }

            Product product = productRepository.GetByBusinessId(productPrice.ProductBusinessId);
            product.AddPrice(productPrice.Price, productPrice.EffectiveDateUtc);
            productRepository.Update(product);
        }
    }
}