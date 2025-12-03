using APICatalogo.Conf;

var builder = WebApplication.CreateBuilder(args);

// SERVICES

builder.Services
    .AddDbContextConf(builder.Configuration)
    .AddIdentityConf()            
    .AddAuthenticationConf(builder.Configuration) 
    .AddControllersConf()
    .AddEndpointsApiExplorer()
    .AddSwaggerConf()
    .AddAuthorizationConf()
    .AddCorsConf()
    .AddRateLimiterConf(builder.Configuration)
    .AddFileUploadConf();

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
    //.UseHttpsRedirection() // Comentado para desenvolvimento - React Native precisa de HTTP
    .UseStaticFiles()
    .UseRouting()
    .UseRateLimiterConf()
    .UseCorsConf()
    .UseAuthenticationConf()
    .UseAuthorizationConf();
app.MapControllers();

app.Run();
