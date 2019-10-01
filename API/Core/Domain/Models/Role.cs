using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Core.Domain.Models
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; private set; }

        public Role()
        {
            UserRoles = new List<UserRole>();
        }
    }
}