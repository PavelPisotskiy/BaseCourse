using Orchard.Mvc.Routes;
using Orchard.WebApi.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BaseCourse.WebService
{
    public class ApiRoutes : IHttpRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new RouteDescriptor[] {
                new HttpRouteDescriptor {
                    Name = "Default Api",
                    Priority = 0,
                    RouteTemplate = "api/{controller}/{action}/{id}",
                    Defaults = new {
                        area = "BaseCourse.WebService",
                        action = "GetCart",
                        controller = "Order",
                        id = RouteParameter.Optional
                    }
                }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (RouteDescriptor routeDescriptor in GetRoutes())
            {
                routes.Add(routeDescriptor);
            }
        }
    }
}