using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Viewmodels
{
    public class TheedResult
    {
        public TheedResult()
        {
            Parameters = new Dictionary<string, object>();
        }

        [System.Text.Json.Serialization.JsonIgnore]
        public IDictionary<string, object> Parameters { get; set; }
        public Uri PaymentUrl { get; set; }
    }
}
