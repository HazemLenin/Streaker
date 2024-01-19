using System.Net;

namespace Streaker.API.Middlewares
{
    public class HttpExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<HttpExceptionMiddleware> _logger;

        public HttpExceptionMiddleware(ILogger<HttpExceptionMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ApiResponse<object> response = new()
                {
                    Errors = new()
                    {
                        {String.Empty, new() { "Internal server error." }}
                    }
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
