using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{
    /// <summary>
    /// PosNet PosNet servislerine gitmek için kullanılır.
    /// </summary>
    [XmlRoot(ElementName = "posnetRequest")]
    public class PosnetRequest
    {
        /// <summary>
        /// PosNet Üye İşyeri Numarası
        /// </summary>
        [XmlElement(ElementName = "mid")]
        public string Mid { get; set; }

        /// <summary>
        /// PosNet Üye İşyeri Terminal Numarası
        /// </summary>
        [XmlElement(ElementName = "tid")]
        public string Tid { get; set; }


        /// <summary>
        /// Ödemenin finanslaştırılması için kullanılan sınıf
        /// </summary>
        [XmlElement(ElementName = "oosTranData")]
        public OosTranData OosTranData { get; set; }

        /// <summary>
        /// MAC/Kullanıcı Doğrulama Sonuç Sorgulanması  için kullanılır
        /// </summary>
        [XmlElement(ElementName = "oosResolveMerchantData")]
        public OosResolveMerchantData OosResolveMerchantData { get; set; }

        [XmlElement(ElementName = "oosRequestData")]
        public OosRequestData OosRequestData { get; set; }

        /// <summary>
        /// Posnet sisteminde işlemin gerçekleştiği zamanın reponse içerisinde yer almasını sağlar.
        /// </summary>
        [XmlElement(ElementName = "tranDateRequired")]
        public string TranDateRequired { get; set; }

        /// <summary>
        /// Satış yapılırken kullanılan sınıf
        /// </summary>
        [XmlElement(ElementName = "sale")]
        public Sale Sale { get; set; }

        /// <summary>
        /// İptal işlemleri için kullanılır
        /// </summary>
        [XmlElement("reverse")]
        public ReverseInfo ReverseInfo { get; set; }

        [XmlElement("return")]
        public ReturnInfo ReturnInfo { get; set; }
    }
}
