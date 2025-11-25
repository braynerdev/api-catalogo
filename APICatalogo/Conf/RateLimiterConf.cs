using APICatalogo.RateLimitOptions;
using Microsoft.Extensions.Configuration;
using System.Threading.RateLimiting;

namespace APICatalogo.Conf
{
    public static class RateLimiterConf
    {
        public static IServiceCollection AddRateLimiterConf(this IServiceCollection services, IConfiguration configuration)
        {
            var myoptions = new MyRateLimitOptions();
            configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myoptions);

            services.AddRateLimiter(optionsRateLimit =>
            {
                optionsRateLimit.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                optionsRateLimit.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(HttpContext =>
                                                 RateLimitPartition.GetFixedWindowLimiter(
                                                    partitionKey: HttpContext.User.Identity.Name ??
                                                    HttpContext.Request.Headers.Host.ToString(),
                                                    factory: partition => new FixedWindowRateLimiterOptions
                                                    {
                                                        AutoReplenishment = myoptions.AutoReplenishment,
                                                        PermitLimit = myoptions.PermitLimit,
                                                        QueueLimit = myoptions.QueueLimit,
                                                        Window = TimeSpan.FromSeconds(myoptions.Window)
                                                    }));
            });

            return services;
        }

        public static IApplicationBuilder UseRateLimiterConf(this IApplicationBuilder app)
        {
            app.UseRateLimiter();
            return app;
        }
    }
}
  