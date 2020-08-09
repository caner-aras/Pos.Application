using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Gateways : BaseEntity
    {
        public int BankId { get; set; }

        public virtual Clients Client { get; set; }
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }

        public bool Default { get; set; }

        public ICollection<Merchant> Merchants { get; set; }
        public MerchantUri MerchantUri { get; set; }
    }
}
