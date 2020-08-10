using System.Xml.Serialization;

namespace Gateway.Core.Models.Garanti
{
    /// <summary>
    /// Corporate payment inquiry
    /// <para lang="tr">Kurum ödeme sorgulaması</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSUtilityPaymentInq : GVPSTransactionInqBase
    {
    }
}