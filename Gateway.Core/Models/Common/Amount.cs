using Application.Enums;
using Application.Helpers.FormGateway;

namespace Gateway.Core.Models.Asseco
{
    public class Amount
    {
        /// <summary>
        /// İşlem tutarı
        /// </summary>
        [FormElement("amount")]
        public string Value { get; set; }

        /// <summary>
        /// ISO para birimi kodu (TL için 949)
        /// </summary>
        [FormElement("currency")]
        public CurrencyCodes Currency { get; set; }
    }
}
