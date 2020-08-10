using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{
    [XmlRoot(ElementName = "sale")]
    public class Sale
    {

        /// <summary>
        /// Alışveriş tutarı – Kuruş cinsinden Ör : 12.34 TL için 1234 olarak set edilmelidir.
        /// </summary>
        [XmlElement(ElementName = "amount")]
        public string Amount { get; set; }


        /// <summary>
        /// Kredi kartı numarası
        /// </summary>
        [XmlElement(ElementName = "ccno")]
        public string CreditCardNumber { get; set; }


        /// <summary>
        /// Para birimi – “TL, US, EU”
        /// </summary>
        [XmlElement(ElementName = "currencyCode")]
        public string Currency { get; set; }

        /// <summary>
        ///  Kredi kartı son kullanım tarihi – Formatı yıl ay olacak şekilde YYAA
        /// </summary>
        [XmlElement(ElementName = "expDate")]
        public string ExpDate { get; set; }

        /// <summary>
        /// Kredi kartı güvenlik numarası – CVV2
        /// </summary>
        [XmlElement(ElementName = "cvc")]
        public string Cvc { get; set; }

        /// <summary>
        ///Alışveriş taksit sayısı
        ///Peşin İşlem için “00” kullanılmalıdır.
        ///2 taksitli işlem için “02” kullanılmalıdır.
        /// </summary>
        [XmlElement(ElementName = "installment")]
        public string Installment { get; set; }


        /// <summary>
        /// Alışveriş sipariş numarası. 24 haneli alphanumeric
        /// </summary>
        [XmlElement(ElementName = "orderID")]
        public string OrderID { get; set; }


        /// <summary>
        /// Joker Vadaa kampanya tipi (kişiye özel işlem kodu). Kart numarası ile sorgulama yapılarak kişiye özel işlem listesi sorgulanarak kullanılmalıdır. 
        /// Opsiyoneldir. Eğer bir değer set edilmiyorsa xml içerisinde bulundurulmamalıdır.
        /// 1: Ek Taksit
        /// 2: Taksit Atlatma
        /// 3: Ekstra Puan
        /// 4: Kontur Kazanım
        /// 5: Ekstre Erteleme
        /// 6: Özel Vade Farkı
        /// </summary>
        [XmlElement(ElementName = "koiCode")]
        public string KoiCode { get; set; }


        /// <summary>
        /// Posnet hizmeti bir ödeme aracısı (payment facilitator) tarafından kullanılıyorsa ödeme aracısı firma Posnet sistemine kendi müşterilerini tanımlattığı bilgileri bu 3 alan ile göndermelidir. 
        /// Ödeme sağlayıcısı olmayan standart işyerlerinin xml içerisinde bu alanlara yer vermemesi gerekmektedir.
        /// </summary>
        [XmlElement(ElementName = "subMrcId")]
        public string SubMrcId { get; set; }
        [XmlElement(ElementName = "mrcPfId")]
        public string MrcPfId { get; set; }
        [XmlElement(ElementName = "mcc")]
        public string Mcc { get; set; }


        /// <summary>
        /// Ana-Bayi alt bayi ilişkisi ile işlem yapan işyerleri bu 3 alandan herhangi birini ya da tamamını doldurarak işlem gönderdiklerinde mid ve tid alanlarında yer alan ana bayinin tckn/vkn/subdealercode sahipli alt bayisi tespit edilerek işlem yapılır. 
        /// Ana bayi – Alt bayi ilişkisi olmayan standart işyerlerinin xml içerisinde bu alanlara yer vermemesi gerekmektedir.
        /// </summary>
        [XmlElement(ElementName = "tckn")]
        public string Tckn { get; set; }
        [XmlElement(ElementName = "vkn")]
        public string Vkn { get; set; }
        [XmlElement(ElementName = "subDealerCode")]
        public string SubDealerCode { get; set; }
    }
}
