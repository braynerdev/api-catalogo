using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace APICatalogo.Filters
{
    public class ApiLoggingFilter : IAsyncActionFilter
    {
        private readonly AppDbContext _context;

        public ApiLoggingFilter(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string tipo = context.HttpContext.Request.Method switch
            {
                "GET" => "Select",
                "POST" => "Insert",
                "PUT" or "PATCH" => "Update",
                "DELETE" => "Delete",
                _ => "Other"
            };

            string userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            string tableName = null;
            int? registerId = null;

            if (context.ActionArguments.Any())
            {
                var firstArg = context.ActionArguments.First().Value;
                if (firstArg != null)
                {
                    tableName = firstArg.GetType().Name;
                    var idProp = firstArg.GetType().GetProperty("Id");
                    if (idProp != null)
                    {
                        registerId = idProp.GetValue(firstArg) as int?;
                    }
                }
            }

            _context.Loggers.Add(new Logger
            {
                Tipo = tipo,
                StatusCode = null,
                TableName = tableName,
                RegisterId = registerId,
                UserId = userId
            });

            await _context.SaveChangesAsync();

            var executedContext = await next();

            _context.Loggers.Add(new Logger
            {
                Tipo = tipo,
                StatusCode = executedContext.HttpContext.Response.StatusCode,
                TableName = tableName,
                RegisterId = registerId,
                UserId = userId
            });
            await _context.SaveChangesAsync();
        }
    }
}
