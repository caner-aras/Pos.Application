using System.Xml.Serialization;

namespace Gateway.Core.Models.Garanti
{
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [XmlType(AnonymousType = false)]
    public class GVPSCommentList
    {
        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Comment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddresscount")]
        public GVPSComment[] Comment { get; set; }
    }
}