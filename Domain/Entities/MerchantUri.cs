using Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class MerchantUri : BaseEntity
    {
        public Gateways Gateway { get; set; }
        [ForeignKey("GatewayId")]
        public int GatewayId { get; set; }

        public string GatewayUri { get; set; }

        public string GateUri { get; set; }

    }
}
