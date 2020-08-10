using System.Xml.Serialization;

namespace Domain.Enums
{
    public enum ProcessType
    {
        [XmlEnum("Auth")]
        Auth,


        /// <summary>
        /// Satış
        ///  Eğer OrderId alanı doldurulmazsa , sistem otomatik OrderId üretir ve yanıt mesajında geri dönülür
        /// </summary>
        [XmlEnum("sales")]
        Sales,

        /// <summary>
        /// İade
        /// “OrderId” alanı, iadesi yapılacak işlemin OrderId değeri ile doldurulmalıdır.
        /// </summary>
        [XmlEnum("Sale")]
        SaleWithPosNet,


        /// <summary>
        /// Ön onay
        /// </summary>
        [XmlEnum("preauth")]
        PreAuth,

        /// <summary>
        /// Ön otorizasyon kapama yapmak için, “Type” alanına “PostAuth” ataması yapılır. “OrderId” alanı, 
        /// kapaması yapılacak ön otorizasyon işleminin OrderId değeri ile doldurulmalıdır
        /// </summary>
        [XmlEnum("postauth")]
        PostAuth,

        /// <summary>
        /// İptal
        /// İadesi yapılacak orijinal işlemin OrderId ya da TransId doldurulmalıdır. 
        /// </summary>
        [XmlEnum("void")]
        Void,

        /// <summary>
        /// İade
        /// “OrderId” alanı, iadesi yapılacak işlemin OrderId değeri ile doldurulmalıdır.
        /// </summary>
        [XmlEnum("refund")]
        Refund
    }
}
