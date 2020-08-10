using Application.Enums;
using Application.Helpers.FormGateway;

namespace Gateway.Core.Models.Asseco
{
    public class Merchant
    {
        /// <summary>
        /// Üye iş yeri numarası (Nestpay tarafından verilen numaradır.)
        /// </summary>
        [FormElement("clientid")]
        public string ClientId { get; set; }

        /// <summary>
        /// 3d_pay_hosting (Mağaza da kullanılan ödeme model tipi)
        /// </summary>
        [FormElement("storetype")]
        public string StoreType { get; set; }


        /// <summary>
        /// Banka tarafından onay sonrası verilecek bir storek key değeri
        /// </summary>
        [FormElement("storekey")]
        public string StoreKey { get; set; }


        /// <summary>
        /// Sipariş numarası
        /// </summary>
        [FormElement("oid")]
        public string OrderId { get; set; }

        /// <summary>
        /// Nestpay Ödeme Geçidine gelen başarılı işlem bilgilendirmesini üye iş yeri tarafındaki başarılı işlem bildirimi için önceden belirlenmiş olan URL' e iletilir.
        /// </summary>
        [FormElement("okUrl")]
        public string SuccessUrl { get; set; }

        /// <summary>
        /// : Nestpay Ödeme Geçidine gelen başarısız işlem bilgilendirmesini üye iş yeri tarafındaki başarısız işlem bildirimi için önceden belirlenmiş olan URL' e iletilir.
        /// </summary>
        [FormElement("failUrl")]
        public string FailUrl { get; set; }


        /// <summary>
        /// Bankanın ortak ödeme sayfasında işlem sonucu görülürken geçecek saniye
        /// </summary>
        [FormElement("refreshtime")]
        public int RefreshTime { get; set; }

        [FormElement("transactionType")]
        public string TransactionType { get; set; }

        /// <summary>
        /// Api Kullanıcı Adı
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Api şifresi
        /// </summary>
        public string Password { get; set; }

        public ProcessType ProcessType { get; set; }

        public FrequencyType FrequencyType { get; set; }

        public RecurringType RecurringType { get; set; }

        public RequestMode RequestMode { get; set; }

        public SecurityLevel RequestType { get; set; }
    }
}
