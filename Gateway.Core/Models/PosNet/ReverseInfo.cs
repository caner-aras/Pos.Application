using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{

    /// <summary>
    /// Gün içinde yapılan bir işlemi iptal etmek için kullanılır. İptal edilen işlemler finansal bir değer kazanmazlar ve müşteri ekstresinde hiçbir şekilde görünmezler. 
    /// İptal işleminin ardından müşterinin kredi kartının limiti de en geç gün sonunda olmak üzere işlem tutarı oranında arttırılır. 
    /// Gün sonunda satış işlemleri finansallaşır ve finansallaşmış işlemlerin iptali ancak iade işlemiyle mümkün olmaktadır. 
    /// </summary>
    [XmlRoot(ElementName = "reverse")]
    public class ReverseInfo
    {
        /// <summary>
        /// İptal edilecek işleminin tipi bu alanda set edilir. 
        /// Satışın iptali, provizyonun iptali finansallaştırmanın iptali, puan kullanımın iptali, VFT işleminin iptali, iadenin iptali olarak kullanılır.
        /// Satis: sale
        /// Provizyon: auth
        /// Finansallastirma: capt
        /// Puan Kullanim: pointUsage
        /// VFT Islemi: vftTransaction
        /// Iade Islemi: return
        /// </summary>
        [XmlElement(ElementName = "transaction")]
        public string Transaction { get; set; }


        /// <summary>
        /// Sistem tarafındaki işlemin tekil Id’sidir. İlgili işlem için servis dönüşünden elde edilerek kullanılır.
        /// </summary>
        [XmlElement(ElementName = "hostLogKey")]
        public string HostLogKey { get; set; }


        /// <summary>
        /// Alışveriş sipariş numarası. Opsiyoneldir. 
        /// Eğer sisteminizde hostlogkey değerini tutmuyorsanız, iptal işlemini orijinal işlemin sipariş numarasını kullanarak da yapabilirsiniz. 
        /// Ancak bu yöntem performans açısından hostlogkey kullanımından daha kötüdür. Hostlogkey kullanılıyorsa bu alana xml içerisinde yer verilmemelidir
        /// </summary>
        [XmlElement(ElementName = "orderID")]
        public string OrderID { get; set; }


        /// <summary>
        /// Sistem yetkilendirmesine istinaden oluşturulan yetki kodudur. Vade Farklı İşlem (VFT) servis dönüşünde elde edilerek kullanılır. 
        /// VFT işlem iptali için bu alan zorunludur, diğer iptal işlemleri için xml içerisinde yer verilmemelidir.
        /// </summary>
        [XmlElement(ElementName = "authCode")]
        public string AuthCode { get; set; }
    }

}
