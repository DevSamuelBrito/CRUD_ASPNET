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
        /// <param name="app">A instância de `WebApplication` usada para criar o escopo de serviços.</param>
        /// <returns>Retorna a mesma instância de `WebApplication` para permitir encadeamento.</returns>
        public static WebApplication ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            return app;
        }
    }
}