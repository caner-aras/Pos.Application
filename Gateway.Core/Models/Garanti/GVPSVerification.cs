using System.Xml.Serialization;

namespace Gateway.Core.Models.Garanti
{
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [XmlType(AnonymousType =true)]
    public class GVPSVerification
    {
        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Identity")]
        public string Identity { get; set; }
    }
}