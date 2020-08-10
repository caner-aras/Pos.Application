using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities.Auth;
using Domain.Viewmodels;

namespace Application.Interfaces {
    public interface IJwtFactory {
        Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync (ApplicationUser user);
        ClaimsPrincipal GetPrincipalFromToken (string token);
    }
}