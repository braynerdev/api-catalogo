using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitação")
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
