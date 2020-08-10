using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{
    [XmlRoot(ElementName = "posnetResponse")]
    public class PosnetResponse
    {
        [XmlElement(ElementName = "approved")]
        public string Approved { get; set; }
        [XmlElement(ElementName = "respCode")]
        public string RespCode { get; set; }
        [XmlElement(ElementName = "respText")]
        public string RespText { get; set; }
        [XmlElement(ElementName = "mac")]
        public string Mac { get; set; }
        [XmlElement(ElementName = "hostlogkey")]
        public string Hostlogkey { get; set; }
        [XmlElement(ElementName = "authCode")]
        public string AuthCode { get; set; }
        [XmlElement(ElementName = "instInfo")]
        public InstInfo InstInfo { get; set; }
        [XmlElement(ElementName = "pointInfo")]
        public PointInfo PointInfo { get; set; }
        [XmlElement(ElementName = "oosRequestDataResponse")]
        public OosRequestDataResponse OosRequestDataResponse { get; set; }
        [XmlElement(ElementName = "oosResolveMerchantDataResponse")]
        public OosResolveMerchantDataResponse OosResolveMerchantDataResponse { get; set; }
    }
}
