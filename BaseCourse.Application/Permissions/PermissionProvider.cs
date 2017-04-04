using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Permissions
{
    public class PermissionProvider : IPermissionProvider
    {
        public static readonly Permission GetOrdersOfCurrentCustomer = new Permission() { Description = "Get the orders of the current customer", Name = "GetOrdersOfCurrentCustomer" };
        public static readonly Permission GetAllOrders = new Permission() { Description = "Get all orders of the customers", Name = "GetAllOrder" };
        public static readonly Permission AcceptOrderOfAnyCustomer = new Permission() { Description = "Accept the order of the any customer", Name = "AcceptOrderOfAnyCustomer" };
        public static readonly Permission RejectOrderOfCurrentCustomer = new Permission() { Description = "Reject the order of the current customer", Name = "RejectOrderOfCurrentCustomer" };
        public static readonly Permission RejectOrderOfAnyCustomer = new Permission() { Description = "Reject the order of the any customer", Name = "RejectOrderOfAnyCustomer" };
        public static readonly Permission CreateOrderForCurrentCustomer = new Permission() { Description = "Create the order for the current customer", Name = "CreateOrderForCurrentCustomer" };
        public static readonly Permission ModifyOrderOfCurrentCustomer = new Permission() { Description = "Modify the order of the current customer", Name = "ModifyOrderOfCurrentCustomer" };
        public static readonly Permission CheckoutOrderOfCurrentCustomer = new Permission() { Description = "Checkout the order of the current customer", Name = "CheckoutOrderOfCurrentCustomer" };
        public static readonly Permission GetProducts = new Permission() { Description = "Get products", Name = "GetProducts" };
        public static readonly Permission CreateProducts = new Permission() { Description = "Create products", Name = "CreateProducts" };
        public static readonly Permission AddProductPrice = new Permission() { Description = "Add product price", Name = "AddProductPrice" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] 
            {
                GetOrdersOfCurrentCustomer,
                GetAllOrders,
                AcceptOrderOfAnyCustomer,
                RejectOrderOfCurrentCustomer,
                RejectOrderOfAnyCustomer,
                CreateOrderForCurrentCustomer,
                ModifyOrderOfCurrentCustomer,
                CheckoutOrderOfCurrentCustomer,
                GetProducts,
                CreateProducts,
                AddProductPrice
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] 
            {
                new PermissionStereotype 
                {
                    Name = "Technologist",
                    Permissions = new[] 
                    {
                        GetAllOrders,
                        AcceptOrderOfAnyCustomer,
                        RejectOrderOfAnyCustomer,
                        GetProducts,
                        CreateProducts,
                        AddProductPrice
                    }
                },
                new PermissionStereotype 
                {
                    Name = "Customer",
                    Permissions = new[] 
                    {
                        GetOrdersOfCurrentCustomer,
                        RejectOrderOfCurrentCustomer,
                        CreateOrderForCurrentCustomer,
                        ModifyOrderOfCurrentCustomer,
                        CheckoutOrderOfCurrentCustomer
                    }
                }
            };
        }
    }
}