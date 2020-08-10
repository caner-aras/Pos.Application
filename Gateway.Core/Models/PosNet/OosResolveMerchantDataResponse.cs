using System.Xml.Serialization;

namespace Gateway.Core.Model.PosNet
{
    [XmlRoot(ElementName="oosResolveMerchantDataResponse")]
	public class OosResolveMerchantDataResponse {
		[XmlElement(ElementName="xid")]
		public string Xid { get; set; }
		[XmlElement(ElementName="amount")]
		public string Amount { get; set; }
		[XmlElement(ElementName="currency")]
		public string Currency { get; set; }
		[XmlElement(ElementName="installment")]
		public string Installment { get; set; }
		[XmlElement(ElementName="point")]
		public string Point { get; set; }
		[XmlElement(ElementName="pointAmount")]
		public string PointAmount { get; set; }
		[XmlElement(ElementName="txStatus")]
		public string TxStatus { get; set; }
		[XmlElement(ElementName="mdStatus")]
		public string MdStatus { get; set; }
		[XmlElement(ElementName="mdErrorMessage")]
		public string MdErrorMessage { get; set; }
		[XmlElement(ElementName="mac")]
		public string Mac { get; set; }
	}
}
