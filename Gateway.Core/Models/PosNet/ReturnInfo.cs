using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{

    /// <summary>
    /// Bu işlem geçmişte yapılan satış veya finansallaştırmaları tamamen iptal etmek veya sadece belirli bir tutarını iade etmektedir. 
    /// Kart ekstresinde ayrı bir işlem olarak görünür ve iptal edilen işlemin ekstredeki kaydını silmez.
    /// İade işlemlerinde iade edilecek tutar, orijinal işlem tutarını ve daha önce aynı orijinal işlem üzerinde yapılmış iadelerin tutarlarının toplamını geçemez. 
    /// Örneğin 10 TL'lik bir işlemin ilk iadesinde 3 TL iade ettiyseniz 2. iadede de en fazla 7 TL iade edebilirsiniz. 
    /// Bir işlemin iadelerinin toplam tutarı orijinal işlemin tutarına ulaşmadığı sürece iade işlemi yapılabilir. 
    /// İşlemlerin yapıldıkları gün iade edilmeleri de mümkündür. Böylece iptal işlemlerinin aksine, işlemin sadece bir kısmı iade edilebilir. 
    /// 
    /// </summary>
    [XmlRoot(ElementName = "return")]
    public class ReturnInfo
    {
        /// <summary>
        /// Alışveriş tutarı – Kuruş cinsinden Ör : 12.34 TL için 1234 olarak set edilmelidir
        /// </summary>
        [XmlElement(ElementName = "amount")]
        public string Amount { get; set; }


        /// <summary>
        /// Posnet sisteminde işlemin gerçekleştiği zamanın response içerisinde yer almasını sağlar. Destek ihtiyacında bu bilgi süreci hızlandıracaktır.
        /// </summary>
        [XmlElement(ElementName = "tranDateRequired")]
        public string TranDateRequired { get; set; }

        /// <summary>
        /// Para birimi – “TL, US, EU”
        /// </summary>
        [XmlElement(ElementName = "currencyCode")]
        public string CurrencyCode { get; set; }


        /// <summary>
        /// Alışveriş sipariş numarası. Opsiyoneldir. 
        /// Eğer sisteminizde hostlogkey değerini tutmuyorsanız, iptal işlemini orijinal işlemin sipariş numarasını kullanarak da yapabilirsiniz. 
        /// Ancak bu yöntem performans açısından hostlogkey kullanımından daha kötüdür. Hostlogkey kullanılıyorsa bu alana xml içerisinde yer verilmemelidir
        /// </summary>
        [XmlElement(ElementName = "hostLogKey")]
        public string hostLogKey { get; set; }

        /// <summary>
        /// Alışveriş sipariş numarası. Opsiyoneldir. 
        /// Eğer işyeri sisteminde hostlogkey değerini tutulmuyorsa, iptal işlemini orijinal işlemin sipariş numarası kullanarak da yapılabilir. 
        /// Eğer 3D Secure ödeme yönetimi ile finansallaştırılmış bir işlemin iadesi yapılıyorsa 20 haneli orderId önüne “TDSC” koyularak 24 haneye tamamlanması gerekmektedir. 
        /// Örn: TDSCYKB_0000190526121122 Bu yöntem performans açısından hostlogkey kullanımından daha kötüdür.
        /// Hostlogkey kullanılıyorsa bu alana xml içerisinde yer verilmemelidir.
        /// </summary>
        [XmlElement(ElementName ="orderid")]
        public string OrderId { get; set; }

    }
}
