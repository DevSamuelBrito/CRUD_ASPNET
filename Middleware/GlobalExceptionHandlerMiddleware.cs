using System.Net;
using System.Text.Json;

namespace CRUD_ASPNET.Middleware
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
                // Tenta executar o próximo middleware na pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Se algo der errado, captura aqui e trata
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Loga o erro completo com stack trace
            _logger.LogError(exception, "Erro não tratado: {Message}", exception.Message);

            // Define o status code baseado no tipo de exceção
            var statusCode = exception switch
            {
                InvalidOperationException => HttpStatusCode.NotFound,     // 404
                ArgumentException => HttpStatusCode.BadRequest,            // 400
                UnauthorizedAccessException => HttpStatusCode.Unauthorized, // 401
                _ => HttpStatusCode.InternalServerError                    // 500
            };

            // Cria a resposta padronizada
            var response = new
            {
                StatusCode = (int)statusCode,
                Message = exception.Message,
                // Em produção, não expõe detalhes internos
                Detail = context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment()
                    ? exception.StackTrace
                    : "Ocorreu um erro interno. Entre em contato com o suporte."
            };

            // Configura a resposta HTTP
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            // Serializa e retorna o JSON
            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
