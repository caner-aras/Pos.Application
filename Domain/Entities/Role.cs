using Domain.Entities.Auth;
using Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public Guid UserId { get; set; }
        public Clients Client { get; set; }
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
    }
}
