using System.Xml.Serialization;

namespace Domain.Enums
{
    [XmlType(AnonymousType = true)]
    public enum RequestMode
    {
        /// <summary>
        /// Test
        /// </summary>
        [XmlEnum("Test")]
        TEST,

        /// <summary>
        /// Gerçek/Canlı/Üretim
        /// </summary>
        [XmlEnum("Prod")]
        PROD,
    }
}
