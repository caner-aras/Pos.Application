using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transactions : BaseEntity
    {
        public string TransactionId { get; set; }

        public Rates Rates { get; set; }
        [ForeignKey("RateId")]
        public int RateId { get; set; }

        /// <summary>
        /// Çalışma ortamı (test, canlı gibi)
        /// Gateway.Model.Enums.RequestMode
        /// </summary>
        public int RequestMode { get; set; }

        /// <summary>
        /// İşlemin tipi 
        /// Gateway.Model.Enums.SecurityLevel
        /// </summary>
        public int SecurityLevel { get; set; }

        /// <summary>
        /// İşlem tipi, satış, iade, iptal vs. gibi durumlar
        /// Gateway.Model.Enums.ProcessType
        /// </summary>
        public int ProcessType { get; set; }


        /// <summary>
        /// Banka tarafından verilen işlem onay numarası
        /// </summary>
        public string ConfirmationCode { get; set; }

        /// <summary>
        /// İşlem tutarı
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Borç Tutarı
        /// </summary>
        public decimal Debt { get; set; }


        /// <summary>
        /// Komisyon
        /// </summary>
        public decimal Commission { get; set; }


        /// <summary>
        /// İstemci tarafından sağlanan unique sipariş numarası
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Banka tarafından dönen mesaj
        /// </summary>
        public string Message { get; set; }


        public bool Approved { get; set; }

        /// <summary>
        /// İşleme ait üst işlem Id'si
        /// </summary>
        [ForeignKey("ParentId")]
        public int? ParentId { get; set; }
        public virtual Transactions Parent { get; set; }
    }
}
