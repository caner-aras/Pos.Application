using System.Xml.Serialization;

namespace Gateway.Core.Models.Garanti
{
    /// <summary>
    /// Corporate payment cancellation inquiry
    /// <para lang="tr">Kurumsal ödeme iptali sorgusu</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSUtilityPaymentVoidInq : GVPSTransactionInqBase 
    {
    }
}