using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MultiTenancy.Collections;

namespace MultiTenancy.Hosting.Abstractions
{
    public class TenantMiddleware
    {
        public const string HeaderKey = "TenantId";
        private readonly RequestDelegate _next;
        private readonly ITenantCollection _tenants;

        public TenantMiddleware(RequestDelegate next, ITenantCollection tenants)
        {
            _next = next;
            _tenants = tenants;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var id = GetTenantId(context);
            var tenant = _tenants.FirstOrDefault(id);

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

        private string GetTenantId(HttpContext httpContext)
        {
            var headers = httpContext?.Request?.Headers?[HeaderKey];
            if (headers.HasValue && headers.Value.Any())
            {
                var company = headers.Value[0];
                return company;
            }
            else
            {
                //_logger.LogCritical($"request sem {HeaderKey}");
            }
            return "";
        }
    }
}