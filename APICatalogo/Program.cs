using APICatalogo.Conf;
using APICatalogo.Context;
using APICatalogo.DTOs.Map;
using APICatalogo.Extencion;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.RateLimitOptions;
using APICatalogo.Repositories;
using APICatalogo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

// SERVICES

builder.Services
    .AddControllersConf()
    .AddEndpointsApiExplorer()
    .AddSwaggerConf()
    .AddAuthorizationConf()
    .AddDbContextConf(builder.Configuration)
    .AddAuthenticationConf(builder.Configuration)
    .AddCorsConf()
    .AddRateLimiterConf(builder.Configuration)
    .AddFileUploadConf()
    .AddIdentityConf();

// DEIXANDO AQUI CASO PRECISE EM ALGUM PROJETO
//builder.WebHost
//    .AddKestrelUploadLimitConf();


// DI
builder.Services.AddDependecyInjectionConf();


// PIPELINE
var app = builder.Build();

app
    .UseSwaggerConf()
    //.UseHsts()
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseRateLimiterConf()
    .UseCorsConf()
    .UseAuthenticationConf()
    .UseAuthorizationConf();
    //.MapControllers();

app.Run();
