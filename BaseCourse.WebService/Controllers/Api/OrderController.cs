using BaseCourse.Application.Exceptions;
using BaseCourse.Application.Interfaces;
using BaseCourse.Application.Models.Dto;
using BaseCourse.Application.Permissions;
using BaseCourse.WebService.Models.Dto;
using Orchard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BaseCourse.WebService.Controllers.Api
{
    [Authorize]
    public class OrderController : ApiController
    {
        private readonly IOrderService orderService;
        private readonly IOrchardServices orchardService;

        public OrderController(IOrderService orderService, IOrchardServices orchardServices)
        {
            this.orchardService = orchardServices;
            this.orderService = orderService;
        }

        [HttpGet, Route("api/Order/GetCart")]
        public HttpResponseMessage GetCart()
        {
            try
            {
                var currentOrder = orderService.GetCart();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, cart = currentOrder });
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }

        [HttpGet, Route("api/Order/GetCurrentCustomerOrders")]
        public HttpResponseMessage GetCurrentCustomerOrders()
        {
            try
            {
                var orders = orderService.GetCurrentCustomerOrders();
                return Request.CreateResponse(HttpStatusCode.OK, orders);
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }

        [HttpGet, Route("api/Order/Get/{id}")]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var order = orderService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, order);
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
            catch (OrderNotFoundException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(ex.Message));
            }

        }

        [HttpGet, Route("api/Order/GetAllProcessingOrders")]
        public HttpResponseMessage GetAllProcessingOrders()
        {
            try
            {
                var orders = orderService.GetAllProcessingOrders();
                return Request.CreateResponse(HttpStatusCode.OK, orders);
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }

        [HttpPut, Route("api/Order/AddToCart")]
        public HttpResponseMessage AddToCart([FromBody]string productId)
        {
            try
            {
                orderService.AddToCart(productId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }

        }

        [HttpDelete, Route("api/Order/RemoveFromCart")]
        public HttpResponseMessage RemoveFromCart([FromBody]string productId)
        {
            try
            {
                orderService.RemoveFromCart(productId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }

        [HttpPost, Route("api/Order/SetProductQuantity")]
        public HttpResponseMessage SetProductQuantity([FromBody]SetProductQuantityDto setProductQuantityDto)
        {
            if (setProductQuantityDto == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(messages));
            }

            try
            {
                orderService.SetProductQuantity(setProductQuantityDto.ProductBusinessId, setProductQuantityDto.Quantity);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ProductNotFoundException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(ex.Message));
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }

        }

        [HttpPost, Route("api/Order/Checkout")]
        public HttpResponseMessage Checkout([FromBody]string orderId)
        {
            try
            {
                orderService.Checkout(orderId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (OrderNotFoundException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(ex.Message));
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }

        [HttpPost, Route("api/Order/Accept")]
        public HttpResponseMessage Accept([FromBody]string orderId)
        {
            try
            {
                orderService.Accept(orderId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (OrderNotFoundException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(ex.Message));
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }

        [HttpPost, Route("api/Order/Reject")]
        public HttpResponseMessage Reject([FromBody]string orderId)
        {
            try
            {
                orderService.Reject(orderId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (OrderNotFoundException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(ex.Message));
            }
            catch (PermissionException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new HttpError(ex.Message));
            }
        }
    }
}