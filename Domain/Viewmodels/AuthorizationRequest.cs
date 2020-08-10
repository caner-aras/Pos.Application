using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Viewmodels
{
    public class AuthorizationRequest
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public string CvvCode { get; set; }
        public decimal TotalAmount { get; set; }
        
        [JsonIgnore]
        public string CustomerIpAddress { get; set; }

        public string CustomerEmailAddress { get; set; }
        public DateTime OrderDate { get; set; }

        public ProcessType ProcessType { get; set; }
        public RequestMode RequestMode { get; set; }
        public SecurityLevel SecurityLevel { get; set; }

        public string OrderNumber { get; set; }

        public int Installment { get; set; }

        public string AuthNumber { get; set; }

        private string languageIsoCode;
        public string LanguageIsoCode
        {
            get => languageIsoCode ?? "tr";
            set => languageIsoCode = value;
        }

        private string currencyIsoCode;
        public string CurrencyIsoCode
        {
            get => currencyIsoCode ?? "949";
            set => currencyIsoCode = value;
        }


        //[JsonIgnore]
        //public RateResponse Rate { get; set; }
    }
}
