using System.Threading.Tasks;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EuroslackpotException exception)
            {
                await HandleCustomException(context, exception);
            }
        }

        private static Task HandleCustomException(HttpContext context, EuroslackpotException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.StatusCode;

            return context.Response.WriteAsync(new
            {
                context.Response.StatusCode,
                exception.Message,
            }.ToString());
        }
    }
}
