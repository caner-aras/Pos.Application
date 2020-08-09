using Microsoft.AspNetCore.Builder;

namespace Merchant.Api.Middleware
{
    public static class AuthhorizationMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthorizationMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthhorizationMiddleware>();
        }
    }
}
