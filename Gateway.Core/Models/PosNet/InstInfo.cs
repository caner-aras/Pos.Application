using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{
    [XmlRoot(ElementName = "instInfo")]
    public class InstInfo
    {
        [XmlElement(ElementName = "inst1")]
        public string Inst1 { get; set; }
        [XmlElement(ElementName = "amnt1")]
        public string Amnt1 { get; set; }
    }
}
