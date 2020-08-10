using Application.Exceptions;
using Domain;
using Domain.Entities;
using Domain.Viewmodels;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Api.Middleware
{
    public class AuthhorizationMiddleware
    {
        readonly RequestDelegate next;

        public AuthhorizationMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body);
            string path = context.Request.Path;
            if (path.Contains("api"))
            {
                var body = await reader.ReadToEndAsync();
                if (path.Contains("callback"))
                {
                    await next(context);
                }

                RequestBase request = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestBase>(body);

                if (request == null || request.ClientId == 0)
                    throw new BusinessException("ClientId cannot be null or zero");


                if (!string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
                {
                    var accesstoken = context.Request.Headers["Authorization"].ToString().Remove(0, 7);
                    var handler = new JwtSecurityTokenHandler();
                    var tokenreaded = handler.ReadJwtToken(accesstoken);
                    var userid = tokenreaded.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
                    var clients = tokenreaded.Claims.Where(x => x.Type == ClaimTypes.Sid).ToList();

                    if (clients != null)
                        if (!clients.Any(x => x.Value == request.ClientId.ToString()))
                            throw new BusinessException("You're not authorized to this clientId");

                    UserManager.ActiveUserId = Guid.Parse(userid);
                    UserManager.ActiveClients = clients.Select(o => new Clients { Id = int.Parse(o.Value) });
                }

                context.Request.Body.Position = 0;
            }
            await next(context);
        }
    }
}