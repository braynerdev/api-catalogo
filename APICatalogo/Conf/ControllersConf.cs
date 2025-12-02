using APICatalogo.DTOs;
using APICatalogo.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace APICatalogo.Conf
{
    public static class ControllersConf
    {
        public static IServiceCollection AddControllersConf(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilter));
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new { Field = e.Key, Errors = e.Value.Errors.Select(er => er.ErrorMessage) })
                        .ToArray();
                    return new BadRequestObjectResult(Response<object>.Error(errors)); ;
                };
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            }).AddNewtonsoftJson();

            return services;
        }
    }
}
