using System.Xml.Serialization;

namespace Domain.Enums
{
    /// <summary>
    /// Para birimi kodları
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum CurrencyCodes
    {

        /// <summary>
        /// Turkish Lira
        /// </summary>
        [XmlEnum("949")]
        TRL = 949,

        /// <summary>
        /// United State Dollar
        /// </summary>
        [XmlEnum("840")]
        USD = 840,

        /// <summary>
        /// Euro
        /// </summary>
        [XmlEnum("978")]
        EURO = 978,

        /// <summary>
        /// English pound
        /// </summary>
        [XmlEnum("826")]
        GBP = 826,

        /// <summary>
        /// Japan yen
        /// </summary>
        [XmlEnum("392")]
        JPY = 392,
    }
}
