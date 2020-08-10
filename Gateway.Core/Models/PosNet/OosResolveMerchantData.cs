using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{

    /// <summary>
    /// MAC/Kullanıcı Doğrulama Sonuç Sorgulanması  için kullanılır
    /// </summary>
    [XmlRoot(ElementName = "oosResolveMerchantData")]
    public class OosResolveMerchantData
    {
        [XmlElement(ElementName = "bankData")]
        public string BankData { get; set; }
        [XmlElement(ElementName = "merchantData")]
        public string MerchantData { get; set; }
        [XmlElement(ElementName = "sign")]
        public string Sign { get; set; }
        [XmlElement(ElementName = "mac")]
        public string Mac { get; set; }
    }
}
