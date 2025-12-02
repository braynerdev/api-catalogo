using APICatalogo.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine(context);
            var response = Response<object>.Fail("Ocorreu um problema ao tratar a sua solicitação");

            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
            context.ExceptionHandled = true;
        }
    }
}
