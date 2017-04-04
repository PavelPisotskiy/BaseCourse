using BaseCourse.Application.Exceptions;
using BaseCourse.Application.Interfaces;
using BaseCourse.Application.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BaseCourse.WebService.Controllers.Api
{
    public class ProductController : ApiController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet, Route("api/Product/GetProductById/{id}")]
        public HttpResponseMessage GetProductById(string id)
        {
            try
            {
                var product = productService.GetProductById(id);
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }

        [HttpGet, Route("api/Product/GetProducts")]
        public HttpResponseMessage GetProducts()
        {
            try
            {
                var products = productService.GetProducts();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, products = products });
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }

        [HttpPut, Route("api/Product/Create")]
        public HttpResponseMessage Create(ProductDto productDto)
        {
            try
            {
                productService.Create(productDto);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }

        [HttpPost, Route("api/Product/AddProductPrice")]
        public HttpResponseMessage AddProductPrice(ProductPriceDto productPrice)
        {
            try
            {
                productService.AddProductPrice(productPrice);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }
    }
}