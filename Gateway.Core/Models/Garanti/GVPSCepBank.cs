﻿using System.Xml.Serialization;

namespace Gateway.Core.Models.Garanti
{
    /// <summary>
    /// For CepBank operation information
    /// <para lang="tr">CepBank işlem bilgileri için</para> 
    /// </summary>
    [XmlType(AnonymousType =true)]
    public class GVPSCepBank : GVPSCepBankIng
    {
        /// <summary>
        /// Pay source type
        /// <para>Size 1 Byte alfanumeric</para> 
        /// <para lang="tr">Ödeme kaynağı tipi</para> 
        /// </summary>
        [XmlElement("PaymentType", Form = System.Xml.Schema.XmlSchemaForm.Unqualified )]
        public GVPSPaymentTypeEnum PaymentType { get; set; }
    }
}