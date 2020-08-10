using System.Xml.Serialization;

namespace Domain.Enums
{
    [XmlType(AnonymousType = true)]
    public enum RecurringType
    {
        /// <summary>
        /// Tek ödeme
        /// </summary>
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// Tekrarlanan
        /// </summary>
        [XmlEnum("R")]
        Repetitive
    }
}
