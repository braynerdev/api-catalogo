using APICatalogo.Models;
using APICatalogo.Context;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Conf
{
    public static class IdentityConf
    {
        public static IServiceCollection AddIdentityConf(this IServiceCollection services)
        {
            services
                .AddIdentity<AplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 1;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
