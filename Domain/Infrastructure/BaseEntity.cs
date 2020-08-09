using Domain.Entities;
using System;

namespace Domain.Infrastructure
{
    public class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual Status Status { get; set; }

        public BaseEntity()
        {
            this.Status = Status.Available;
        }

        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }

    }
}