using System.Text;
using Application.Interfaces;
using Common;
using Domain.Entities.Auth;
using Infrastructure.Auth.JWT;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Infrastructure.Repositories;
using Infrastructure.Service.Merchant.Transactions;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            services.AddDefaultIdentity<Domain.Entities.Auth.ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IUserManager, UserManagerService>();
            services.AddScoped<ITransactionService, TransactionService>();
            //services.AddScoped<ITransactionRepositories, TransactionRepositories>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IJwtFactory, JwtFactory>();

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));
            services.AddMemoryCache();

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            // jwt

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                });
            services.AddAuthorization();
            return services;
        }
    }
}