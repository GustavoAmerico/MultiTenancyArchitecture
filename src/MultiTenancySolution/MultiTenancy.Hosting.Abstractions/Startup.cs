using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MultiTenancy.Hosting.Abstractions
{
    public static class Startup
    {
        public static IServiceCollection AddTenantScoped(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UserTenantScoped(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantMiddleware>();

            return app;
        }
    }
}