using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{
    [XmlRoot(ElementName = "oosRequestData")]
    public class OosRequestData
    {

        /// <summary>
        /// PosNet Üye İşyeri POSNET Numarası
        /// </summary>
        [XmlElement(ElementName = "posnetid")]
        public string Posnetid { get; set; }

        /// <summary>
        /// Tekil alışveriş sipariş numarası – 20 alfa numerik karakter. İşyeri tarafından oluşturulur. 
        /// </summary>
        [XmlElement(ElementName = "XID")]
        public string XID { get; set; }


        /// <summary>
        /// Alışveriş tutarı – Kuruş cinsinden Ör : 12.34 TL için 1234 olarak set edilmelidir
        /// </summary>
        [XmlElement(ElementName = "amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Para birimi – “TL, US, EU”
        /// </summary>
        [XmlElement(ElementName = "currencyCode")]
        public string CurrencyCode { get; set; }


        /// <summary>
        /// Alışveriş taksit sayısı
        /// Peşin İşlem için “00” kullanılmalıdır.
        /// 2 taksitli işlem için “02” kullanılmalıdır. 
        /// </summary>
        [XmlElement(ElementName = "installment")]
        public string Installment { get; set; }


        /// <summary>
        /// İşlem Tipi
        /// Sale  Satış 
        /// Auth Provizyon
        /// WP World Puan Kullanım
        /// SaleWP Satış ve World Puan Kullanım
        /// Vft Vade Farklı Satış
        /// </summary>
        [XmlElement(ElementName = "tranType")]
        public string TranType { get; set; }


        /// <summary>
        /// Müşterinin adı soyadı
        /// </summary>
        [XmlElement(ElementName = "cardHolderName")]
        public string CardHolderName { get; set; }


        /// <summary>
        /// Kredi kartı numarası
        /// </summary>
        [XmlElement(ElementName = "ccno")]
        public string Ccno { get; set; }


        /// <summary>
        /// Kredi kartı güvenlik numarası – CVV2
        /// </summary>
        [XmlElement(ElementName = "cvc")]
        public string Cvc { get; set; }


        /// <summary>
        /// Kredi kartı son kullanım tarihi – Formatı yıl ay olacak şekilde YYAA
        /// </summary>
        [XmlElement(ElementName = "expDate")]
        public string ExpDate { get; set; }
    }
}
