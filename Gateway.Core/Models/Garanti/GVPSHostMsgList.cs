using System.Xml.Serialization;

namespace Gateway.Core.Models.Garanti
{
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSHostMsgList
    {
        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("HostMsg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] HostMsg { get; set; }
    }
}