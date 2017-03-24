using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Permissions
{
    public class Permissions : IPermissionProvider 
    {
        public static readonly Permission TechnologistPermission = new Permission() { Description = "Find, accept, reject orders", Name = "TechnologistPermission" };
        public static readonly Permission CustomerPermission = new Permission() { Description = "Create, modify, delete order" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            throw new NotImplementedException();
        }
    }
}