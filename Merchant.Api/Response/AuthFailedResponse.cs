using System.Collections.Generic;

namespace Merchant.Api.Response {
    public class AuthFailedResponse {
        public IEnumerable<string> Errors { get; set; }
    }
}