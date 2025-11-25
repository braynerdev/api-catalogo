using APICatalogo.Context;
using APICatalogo.DTOs.Map;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Conf
{
    public static class FileUploadConf
    {
        public static IServiceCollection AddFileUploadConf(this IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB
            });

            return services;
        }

        // DEIXANDO AQUI CASO PRECISE EM ALGUM PROJETO
        //public static IWebHostBuilder AddKestrelUploadLimitConf(this IWebHostBuilder builder)
        //{
        //    var size = 50 * 1024 * 1024;  // 50 MB   
        //    builder.ConfigureKestrel(options =>
        //    {
        //        options.Limits.MaxRequestHeadersTotalSize = size;
        //        options.Limits.MaxRequestBodySize = size;
        //        options.Limits.MaxRequestBufferSize = size;             
        //    });

        //    return builder;
        //}
    }
}
