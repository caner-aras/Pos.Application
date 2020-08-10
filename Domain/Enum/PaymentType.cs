using System.Xml.Serialization;

namespace Domain.Enums
{

    [XmlType(AnonymousType =true)]
    public enum PaymentType
    {
        /// <summary>
        /// Kredi kartı
        /// </summary>
        [XmlEnum("K")]
        CreditCard,

        /// <summary>
        /// Debit kart
        /// </summary>
        [XmlEnum("D")]
        DebitCard,
    }
}
