using Application.Exceptions;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Middleware.Exception
{
    public class ExceptionMiddleware
    {
        RequestDelegate next;
        private readonly Func<object, Task> _clearCacheHeadersDelegate;
        public ExceptionMiddleware(RequestDelegate _next)
        {
            _clearCacheHeadersDelegate = ClearCacheHeaders;
            next = _next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (BusinessException ex)
            {
                await HandleAndWrapExceptionAsync(context, ex.Message);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }


        private async Task WriteResponseAsync(HttpContext context, string bodyJson)
        {
            context.Response.Headers.Add("Accept", "application/json");
            context.Response.Headers.Add("Content-Type", "application/json");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS,DELETE,PUT");
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            byte[] data = ASCIIEncoding.ASCII.GetBytes(bodyJson);
            await context.Response.Body.WriteAsync(data, 0, data.Length);
        }

        private async Task HandleAndWrapExceptionAsync(HttpContext httpContext, string exceptionMessage)
        {
            httpContext.Response.OnStarting(_clearCacheHeadersDelegate, httpContext.Response);
            Response<string> response = new Response<string>
            {
                ErrorMessage = new List<string>() { exceptionMessage }
            };
            var responseJson = JsonSerializer.Serialize(response);
            await WriteResponseAsync(httpContext, responseJson);
        }

        private Task ClearCacheHeaders(object state)
        {
            var response = (HttpResponse)state;
            response.Headers[HeaderNames.CacheControl] = "no-cache";
            response.Headers[HeaderNames.Pragma] = "no-cache";
            response.Headers[HeaderNames.Expires] = "-1";
            response.Headers.Remove(HeaderNames.ETag);
            return Task.CompletedTask;
        }
    }
}