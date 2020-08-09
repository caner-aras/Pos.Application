using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public static class UserManager 
    {
        public static IEnumerable<Clients> ActiveClients { get; set; }
        public static Guid ActiveUserId { get; set; }

        public static bool IsLoggin()
        {
            return ActiveUserId != Guid.Empty;
        }
    }
}
