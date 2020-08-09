using System;
using Domain.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
