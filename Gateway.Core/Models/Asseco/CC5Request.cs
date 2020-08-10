using System.Xml.Serialization;

namespace Gateway.Core.Models.Asseco
{
    /// <summary>
    /// APİ İstekleri için
    /// </summary>
    [XmlRoot(ElementName = "CC5Request")]
    public class Cc5Request
    {
        [XmlIgnore]
        public string Endpoint { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Harf ya da rakam, maksimum 255 karakter
        /// </summary>
        [XmlElement(ElementName = "Password")]
        public string Password { get; set; }
       
        /// <summary>
        /// Harf ya da rakam, maksimum 15 karakter
        /// </summary>
        [XmlElement(ElementName = "ClientId")]
        public string ClientId { get; set; }
        

        /// <summary>
        /// Harf ya da rakam, olabilecek değerler {Auth, PreAuth, PostAuth, Void, Credit}
        /// </summary>
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        

        /// <summary>
        /// Müşterinin IP Adresi maksimum 39 karakter
        /// </summary>
        [XmlElement(ElementName = "IPAddress")]
        public string IPAddress { get; set; }
        
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
        
        /// <summary>
        /// Sipariş Numarası Harf ya da rakam, maksimum 64 karakter
        /// </summary>
        [XmlElement(ElementName = "OrderId")]
        public string OrderId { get; set; }
        

        /// <summary>
        /// Grup Numarası Harf ya da rakam, maksimum 64 karakter
        /// </summary>
        [XmlElement(ElementName = "GroupId")]
        public string GroupId { get; set; }
        

        /// <summary>
        /// İşlem Numarası Harf ya da rakam, maksimum 64 karakter
        /// </summary>
        [XmlElement(ElementName = "TransId")]
        public string TransId { get; set; }
        
        /// <summary>
        /// Toplam Tutar Rakam, ondalıklı rakamları ayırmak için “,” veya “.” kullanılır. Karakter kullanılmaz.
        /// </summary>
        [XmlElement(ElementName = "Total")]
        public string Total { get; set; }
        

        /// <summary>
        /// ISO para birimi kodu Rakam, 3 rakam (949 for TL)
        /// </summary>
        [XmlElement(ElementName = "Currency")]
        public int Currency { get; set; }

        /// <summary>
        /// Harf ya da rakam + sembol
        /// </summary>
        [XmlElement(ElementName = "Number")]
        public string Number { get; set; }
        
        [XmlElement(ElementName = "Expires")]
        public string Expires { get; set; }
        
        [XmlElement(ElementName = "Cvv2Val")]
        public string Cvv2Val { get; set; }
        

        /// <summary>
        /// Taksitsiz işlemlerde taksit parameteresi boş olarak gönderilmelidir
        /// </summary>
        [XmlElement(ElementName = "Instalment")]
        public string Instalment { get; set; }
        
        [XmlElement(ElementName = "PayerSecurityLevel")]
        public string PayerSecurityLevel { get; set; }
        
        [XmlElement(ElementName = "PayerTxnId")]
        public string PayerTxnId { get; set; }
        
        [XmlElement(ElementName = "PayerAuthenticationCode")]
        public string PayerAuthenticationCode { get; set; }
        
        [XmlElement(ElementName = "BillTo")]
        public To BillTo { get; set; }
        
        [XmlElement(ElementName = "ShipTo")]
        public To ShipTo { get; set; }
        
        [XmlElement(ElementName = "OrderItemList")]
        public OrderItemList OrderItemList { get; set; }
    }


    public class To
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Company")]
        public string Company { get; set; }

        [XmlElement(ElementName = "Street1")]
        public string Street1 { get; set; }

        [XmlElement(ElementName = "Street2")]
        public string Street2 { get; set; }

        [XmlElement(ElementName = "Street3")]
        public string Street3 { get; set; }

        [XmlElement(ElementName = "City")]
        public string City { get; set; }

        [XmlElement(ElementName = "StateProv")]
        public string StateProv { get; set; }

        [XmlElement(ElementName = "PostalCode")]
        public string PostalCode { get; set; }

        [XmlElement(ElementName = "Country")]
        public string Country { get; set; }

        [XmlElement(ElementName = "TelVoice")]
        public string TelVoice { get; set; }
    }

    [XmlRoot(ElementName = "OrderItem")]
    public class OrderItem
    {
        [XmlElement(ElementName = "ItemNumber")]
        public string ItemNumber { get; set; }

        [XmlElement(ElementName = "ProductCode")]
        public string ProductCode { get; set; }

        [XmlElement(ElementName = "Qty")]
        public string Qty { get; set; }

        [XmlElement(ElementName = "Desc")]
        public string Desc { get; set; }

        [XmlElement(ElementName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Price")]
        public string Price { get; set; }

        [XmlElement(ElementName = "Total")]
        public string Total { get; set; }
    }

    [XmlRoot(ElementName = "OrderItemList")]
    public class OrderItemList
    {
        [XmlElement(ElementName = "OrderItem")]
        public OrderItem OrderItem { get; set; }
    }
}