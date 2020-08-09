using Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Merchant : BaseEntity
    {

        public Gateways Gateway { get; set; }
        [ForeignKey("GatewayId")]
        public int GatewayId { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
