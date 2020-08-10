using System.Xml.Serialization;

namespace Domain.Enums
{
    [XmlType(AnonymousType = true)]
    public enum SecurityLevel
    {
        /// <summary>
        /// 3d doğrulama (Tahsilat yapılmaz)
        /// </summary>
        [XmlEnum("Api")]
        Api,

        /// <summary>
        /// 3d doğrulama, bu seçenek ile tutar tahsil edilmez
        /// </summary>
        [XmlEnum("3d")]
        OnlyVerification,

        /// <summary>
        /// 3d doğrulama ve tahsilat
        /// </summary>
        [XmlEnum("3d_pay")]
        AuthWithVerification,

        /// <summary>
        /// 3d tam doğrulama ve ödeme
        /// </summary>
        [XmlEnum("3d_full")]
        FullVerification,

    }
}
