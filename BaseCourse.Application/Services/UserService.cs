using BaseCourse.Application.Interfaces;
using BaseCourse.Application.Models;
using BaseCourse.Application.Permissions;
using Orchard;
using Orchard.Security;
using Orchard.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IOrchardServices orchardService;

        public UserService(IOrchardServices orchardServices)
        {
            this.orchardService = orchardServices;
        }

        public IUser GetCurrentUser()
        {
            return orchardService.WorkContext.CurrentUser;
        }

        public IUser GetUserById(int id)
        {
            return (IUser)orchardService.ContentManager.Get(id).Get(typeof(IUser));
        }

        public IEnumerable<IUser> Users()
        {
            IEnumerable<IUser> users = orchardService.ContentManager
                .Query()
                .ForPart<IUser>()
                .List();
            return users;
        }

        public bool VerifyUserPermission(Permission permission)
        {
            return orchardService.Authorizer.Authorize(permission);
        }
    }
}