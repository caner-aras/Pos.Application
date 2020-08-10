using System.Threading.Tasks;
using Domain.Viewmodels;

namespace Application.Interfaces {
    public interface IUserManager {
        Task<AuthenticationResult> RegisterAsync (string email, string password);
        Task<AuthenticationResult> LoginAsync (string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync (string token, string refreshToken);
    }
}