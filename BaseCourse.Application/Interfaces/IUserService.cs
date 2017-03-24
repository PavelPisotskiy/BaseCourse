﻿using BaseCourse.Application.Models;
using Orchard;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCourse.Application.Interfaces
{
    public interface IUserService : IDependency
    {
        IUser GetCurrentUser();
        IUser GetUserById(int id);
        IEnumerable<IUser> Users();
        Role GetCurrentUserRole();
        Role GetUserRoleById();
    }
}
