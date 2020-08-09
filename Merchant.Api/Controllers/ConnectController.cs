using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Merchant.Api.Requests;
using Merchant.Api.Response;
using Microsoft.AspNetCore.Mvc;

namespace Merchant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectController : Controller
    {
        private readonly IUserManager _userManager;

        public ConnectController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var authResponse = await _userManager.RegisterAsync(request.Email, request.Password);
            return Json(new Response<object>()
            {
                Data = new
                {
                    authResponse.Token,
                    authResponse.RefreshToken,
                    authResponse.ExpiresIn
                },
                ErrorMessage = authResponse.Errors
            });
        }

        [HttpPost("Token")]
        public async Task<IActionResult> Token(UserLoginRequest request)
        {
            var authResponse = await _userManager.LoginAsync(request.Email, request.Password);

            return Json(new Response<object>()
            {
                Data = new
                {
                    authResponse.Token,
                    authResponse.RefreshToken,
                    authResponse.ExpiresIn
                },
                ErrorMessage= authResponse.Errors
            });
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var authResponse = await _userManager.RefreshTokenAsync(request.Token, request.RefreshToken);

            return Json(new Response<object>()
            {
                Data = new
                {
                    authResponse.Token,
                    authResponse.RefreshToken,
                    authResponse.ExpiresIn
                },
                ErrorMessage = authResponse.Errors
            });
        }
    }
}