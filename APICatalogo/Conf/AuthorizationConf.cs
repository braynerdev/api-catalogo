using APICatalogo.Filters;
using System.Text.Json.Serialization;

namespace APICatalogo.Conf
{
    public static class AuthorizationConf
    {
        public static IServiceCollection AddAuthorizationConf(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
            });

            return services;
        }

        public static IApplicationBuilder UseAuthorizationConf(this IApplicationBuilder app)
        {
            app.UseAuthorization();
            return app;
        }
    }
}
