using CRUD_ASPNET.API.Middleware;
using CRUD_ASPNET.Configuration.Context;
using Microsoft.EntityFrameworkCore;

namespace CRUD_ASPNET.API.Extensions
{
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Aplica as migrações pendentes do EF Core no banco de dados usando um DbContext em escopo.
        /// Deve ser chamado após a construção do `WebApplication` (por exemplo, logo após `builder.Build()`).
        /// </summary>
        /// <param name="builder">A instância de `WebApplication` usada para criar o escopo de serviços.</param>
        /// <returns>Retorna a mesma instância de `WebApplication` para permitir encadeamento.</returns>
        public static WebApplication ApplyMigrations(this WebApplication builder)
        {
            using var scope = builder.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            return builder;
        }
        /// <summary>
        /// Configura e habilita o Swagger/OpenAPI com base na seção de configuração "Swagger".
        /// Lê os valores `RoutePrefix` e `JsonRouteTemplate` e registra o middleware do
        /// Swagger apenas quando a aplicação estiver em ambiente de desenvolvimento.
        /// </summary>
        /// <param name="builder">A instância de `WebApplication` usada para configurar o middleware.</param>
        /// <returns>Retorna a mesma instância de `WebApplication` para permitir encadeamento.</returns>
        public static WebApplication UseSwaggerFromConfiguration(this WebApplication builder)
        {
            var swaggerSettings = builder.Configuration.GetSection("Swagger");
            var routePrefix = swaggerSettings.GetValue<string>("RoutePrefix") ?? "api/docs";
            var jsonRouteTemplate = swaggerSettings.GetValue<string>("JsonRouteTemplate");


            if (builder.Environment.IsDevelopment())
            {

                if (!string.IsNullOrEmpty(jsonRouteTemplate))
                {
                    builder.UseSwagger(c => c.RouteTemplate = jsonRouteTemplate);

                    var jsonEndpoint = "/" + jsonRouteTemplate.Replace("{documentName}", "v1");
                    builder.UseSwaggerUI(options =>
                    {
                        options.RoutePrefix = routePrefix;
                        options.SwaggerEndpoint(jsonEndpoint, "CRUD_ASPNET v1");
                    });
                }
                else
                {
                    builder.UseSwagger();
                    builder.UseSwaggerUI(options =>
                    {
                        options.RoutePrefix = routePrefix;
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD_ASPNET v1");
                    });
                }

            }

            return builder;
        }


        /// <summary>
        /// Configura o pipeline de middlewares da aplicação em uma única chamada.
        /// Executa etapas de inicialização como aplicação de migrações, registro do
        /// middleware global de exceções, configuração do Swagger, redirecionamento HTTPS,
        /// CORS, rate limiting e mapeamento dos controllers.
        /// </summary>
        /// <param name="app">A instância de `WebApplication` que terá o pipeline configurado.</param>
        /// <returns>Retorna a mesma instância de `WebApplication` para permitir encadeamento.</returns>
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.ApplyMigrations();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseSwaggerFromConfiguration();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRateLimiter();

            app.MapControllers();

            return app;
        }
    }
}