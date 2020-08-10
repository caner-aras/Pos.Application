using System.Xml.Serialization;

namespace Gateway.Core.Models.Asseco
{
    [XmlRoot(ElementName = "CC5Response")]
    public class Cc5Response
    {
        [XmlElement(ElementName = "ErrMsg")]
        public string ErrMsg { get; set; }
        [XmlElement(ElementName = "ProcReturnCode")]
        public string ProcReturnCode { get; set; }
        [XmlElement(ElementName = "Response")]
        public string Response { get; set; }
        [XmlElement(ElementName = "OrderId")]
        public string OrderId { get; set; }
        [XmlElement(ElementName = "TransId")]
        public string TransId { get; set; }
        [XmlElement(ElementName = "Extra")]
        public Extra Extra { get; set; }
        [XmlElement(elementName: "HostRefNum")]
        public string HostRefNum { get; set; }
        [XmlElement(elementName: "AuthCode")]
        public string AuthCode { get; set; }
    }
}
