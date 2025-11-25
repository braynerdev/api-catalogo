using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace APICatalogo.Conf
{
    public static class AuthConf
    {
        public static IServiceCollection AddAuthenticationConf(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["JWT:SecretKey"] ?? throw new ArgumentException("Secret Key Inválida!");
            var validAudience = configuration["JWT:ValidAudience"];
            var validIssuer = configuration["JWT:ValidIssuer"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = validAudience,
                    ValidIssuer = validIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Erro de autenticação: {context.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        public static IApplicationBuilder UseAuthenticationConf(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            return app;
        }
    }
}
