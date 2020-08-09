using System.ComponentModel.DataAnnotations;

namespace Merchant.Api.Requests {
    public class RegisterUserRequest {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}