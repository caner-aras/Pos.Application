using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{
    [XmlRoot(ElementName = "oosRequestDataResponse")]
    public class OosRequestDataResponse
    {
        [XmlElement(ElementName = "data1")]
        public string PostNetData1 { get; set; }

        [XmlElement(ElementName = "data2")]
        public string PostNetData2 { get; set; }

        [XmlElement(ElementName = "sign")]
        public string Sign { get; set; }
    }
}
