using Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Log : BaseEntity
    {

        [ForeignKey("TransactionId")]
        public int TransactionId { get; set; }
        public virtual Transactions Transaction { get; set; }

        public string Raw { get; set; }
    }
}
