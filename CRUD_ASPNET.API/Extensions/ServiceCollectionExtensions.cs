using CRUD_ASPNET.Application.Services.Interfaces;
using CRUD_ASPNET.Configuration.Context;
using CRUD_ASPNET.Infra.Repositories.Interfaces;
using CRUD_ASPNET.Repositories;
using CRUD_ASPNET.Services;
using Microsoft.EntityFrameworkCore;

namespace CRUD_ASPNET.API.Extensions;

public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Adds the database service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDatabaseService(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        //db context
        //builder.Services.AddDbContext<AppDbContext>(options =>
        //    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); sqlite
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))); // postgresql

        return services;
    }


    /// <summary>
    /// Adiciona serviços com escopo à coleção de serviços, registrando
    /// repositórios e serviços da camada de aplicação.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada com os serviços registrados.</returns>
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        //repositories
        services.AddScoped<ITaskRepository, TaskRepository>();

        //services
        services.AddScoped<ITaskService, TaskService>();

        return services;
    }

    /// <summary>
    /// Configura e adiciona uma política CORS padrão à coleção de serviços.
    /// Esta política permite requisições de qualquer origem, qualquer método
    /// e qualquer cabeçalho — adequada para APIs públicas durante desenvolvimento.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada com a política CORS registrada.</returns>
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
            policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });

        return services;
    }

    /// <summary>
    /// Configura e adiciona provedores de logging à coleção de serviços,
    /// permitindo o registro de logs no console e no debug durante a execução da aplicação.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <
    public static IServiceCollection AddConfigureLogging(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });

        return services;

    }
}
