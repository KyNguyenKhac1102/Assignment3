using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks;
using ASP_CORE2.Models;
using System;
using System.IO;
using System.Text.Json;

namespace LogMiddlerWare
{
    public class RequestLogMiddlerWare
    {
        private readonly RequestDelegate _next;

        public RequestLogMiddlerWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
                   
            string schema = context.Request.Scheme;
            string host = context.Request.Host.ToString();
            string path = context.Request.Path;
            string query = context.Request.QueryString.ToString();
            var bodyAsText = await new StreamReader(context.Request.Body).ReadToEndAsync();

            var logModel = new LogModel(schema, host, path, query, bodyAsText);
            string jsonString = JsonSerializer.Serialize(logModel);

            File.WriteAllText(@"C:\Rokie\ASP_CORE2\wwwroot\Log\data.json", jsonString);
            Console.WriteLine($"{schema}, {host}, {path}, {query}, {bodyAsText}");
            
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }

    public static class RequestLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLog(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddlerWare>();
        }
    }
}