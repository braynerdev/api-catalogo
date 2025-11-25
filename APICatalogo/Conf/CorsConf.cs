using Microsoft.OpenApi.Models;

namespace APICatalogo.Conf
{
    public static class CorsConf
    {
        public static IServiceCollection AddCorsConf(this IServiceCollection services)
        {
            var OrigensComAcessoPermitido = "_origensComAcessoPermitido";
            services.AddCors(options =>
                options.AddPolicy(name: OrigensComAcessoPermitido,
                    policy =>
                    {
                        policy.WithOrigins("https://apirequest.io");
                    })
            );

            return services;
        }

        public static IApplicationBuilder UseCorsConf(this IApplicationBuilder app)
        {
            var OrigensComAcessoPermitido = "_origensComAcessoPermitido";
            app.UseCors(OrigensComAcessoPermitido);
            return app;
        }
    }
}
