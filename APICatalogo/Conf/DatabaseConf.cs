using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Conf
{
    public static class DatabaseConf
    {

        public static IServiceCollection AddDbContextConf(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")
                                       ?? throw new ArgumentException("Connection string invalid!");

            services.AddDbContext<AppDbContext>(options =>
                 options.UseMySql(connectionString,
                 ServerVersion.AutoDetect(connectionString)
            ));

            return services;
        }
    }
}
