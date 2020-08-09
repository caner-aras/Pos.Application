using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Rates : BaseEntity
    {
        public Gateways Gateway { get; set; }

        [ForeignKey("GatewayId")]
        public int GatewayId { get; set; }

        public int Installment { get; set; }

        public decimal Rate { get ; set; }

        public int CurrencyId { get; set; }

        public virtual bool IsDeleted { get; set; }
    }
}
