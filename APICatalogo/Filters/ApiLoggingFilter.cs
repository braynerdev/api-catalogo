using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace APICatalogo.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly AppDbContext _context;

        public ApiLoggingFilter(AppDbContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _context.Loggers.Add(new Logger("Request", null, context.ModelState.IsValid));
            _context.SaveChanges();
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

            _context.Loggers.Add(new Logger("Response", context.HttpContext.Response.StatusCode, null));
            _context.SaveChanges();
        }
    }
}
