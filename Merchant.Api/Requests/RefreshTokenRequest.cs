using System.ComponentModel.DataAnnotations;

namespace Merchant.Api.Requests
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}