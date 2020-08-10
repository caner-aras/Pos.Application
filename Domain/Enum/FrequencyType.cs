using System.Xml.Serialization;

namespace Domain.Enums
{
    [XmlType(AnonymousType = true)]
    public enum FrequencyType
    {
        /// <summary>
        /// Tek ödeme
        /// </summary>
        [XmlEnum("")]
        Single,

        /// <summary>
        /// Günlük
        /// </summary>
        [XmlEnum("D")]
        Daily,

        /// <summary>
        /// Haftalık
        /// </summary>
        [XmlEnum("W")]
        Weekly,

        /// <summary>
        /// Aylık
        /// </summary>
        [XmlEnum("M")]
        Mounthly,

        /// <summary>
        /// Yıllık
        /// </summary>
        [XmlEnum("Y")]
        Yearly
    }
}
