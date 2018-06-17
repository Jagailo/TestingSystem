using System;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentTestingSystem.Domain.Infrastructure;

namespace StudentTestingSystem.Domain.Models.Account
{
    public class UserRoles : IdentityUserRole, ICreated
    {
        public DateTime Created { get; set; }
    }
}
