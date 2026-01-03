using System.Net;
using System.Text.Json;

namespace CRUD_ASPNET.API.Middleware
{
    /// <summary>
    /// Middleware global que captura TODAS as exceções da aplicação
    /// e retorna respostas padronizadas ao cliente
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Erro não tratado: {Message}", exception.Message);

            var statusCode = exception switch
            {
                InvalidOperationException => HttpStatusCode.NotFound,     // 404
                ArgumentException => HttpStatusCode.BadRequest,            // 400
                UnauthorizedAccessException => HttpStatusCode.Unauthorized, // 401
                _ => HttpStatusCode.InternalServerError                    // 500
            };

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = exception.Message,
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
