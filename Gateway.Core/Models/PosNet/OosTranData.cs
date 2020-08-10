using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{
    
    [XmlRoot(ElementName = "oosTranData")]
    public class OosTranData
    {
        [XmlElement(ElementName = "bankData")]
        public string BankData { get; set; }
        [XmlElement(ElementName = "wpAmount")]
        public string WpAmount { get; set; }
        [XmlElement(ElementName = "mac")]
        public string Mac { get; set; }
    }
}
