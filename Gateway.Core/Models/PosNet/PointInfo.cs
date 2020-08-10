using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{

    [XmlRoot(ElementName = "pointInfo")]
    public class PointInfo
    {
        [XmlElement(ElementName = "point")]
        public string Point { get; set; }
        [XmlElement(ElementName = "pointAmount")]
        public string PointAmount { get; set; }
        [XmlElement(ElementName = "totalPoint")]
        public string TotalPoint { get; set; }
        [XmlElement(ElementName = "totalPointAmount")]
        public string TotalPointAmount { get; set; }
    }
}
