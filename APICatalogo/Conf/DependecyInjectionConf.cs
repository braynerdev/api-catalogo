using APICatalogo.Context;
using APICatalogo.DTOs.Map;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Categoria;
using APICatalogo.Repositories.Produto;
using APICatalogo.Services;
using APICatalogo.Services.Auth;
using APICatalogo.Services.Categoria;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Conf
{
    public static class DependecyInjectionConf
    {
        public static IServiceCollection AddDependecyInjectionConf(this IServiceCollection services)
        {
            services.AddScoped<ApiLoggingFilter>();
            services.AddScoped<ICategoriaRepositorie, CategoriaRepositorie>();
            services.AddScoped<IProdutoRepositorie, ProdutoRepositorie>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, GenerateAccessTokenService>();

            services.AddAutoMapper(typeof(DomainToMappingProfile));

            services.AddIdentity<AplicationUser, IdentityRole>().
                AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
