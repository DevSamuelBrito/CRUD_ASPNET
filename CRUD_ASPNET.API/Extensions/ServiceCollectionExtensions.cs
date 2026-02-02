using CRUD_ASPNET.Application.Services.Interfaces;
using CRUD_ASPNET.Configuration.Context;
using CRUD_ASPNET.Infra.Repositories.Interfaces;
using CRUD_ASPNET.Repositories;
using CRUD_ASPNET.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

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
    /// <returns>A coleção de serviços atualizada com o logging configurado.</returns>
    public static IServiceCollection AddConfigureLogging(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });

        return services;

    }

    /// <summary>
    /// Configura respostas de validação customizadas para requisições com modelo inválido.
    /// Constrói e registra uma fábrica de respostas que retorna um `BadRequestObjectResult`
    /// contendo o status, título e um dicionário com as mensagens de erro por campo.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada com a configuração de respostas de validação.</returns>
    public static IServiceCollection CustomValidationResponses(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value?.Errors.Select(x => x.ErrorMessage ?? string.Empty).ToArray() ?? Array.Empty<string>()
                    );

                var result = new
                {
                    Status = 400,
                    Title = "One or more validation errors occurred.",
                    Errors = errors
                };

                return new BadRequestObjectResult(result);
            };
        });

        return services;
    }

    /// <summary>
    /// Configura e adiciona um limitador de taxa (rate limiter) à coleção de serviços.
    /// Define um limitador global por endereço IP utilizando janela fixa e registra um
    /// limitador nomeado "strict" mais restritivo. Útil para prevenir abuso e reduzir
    /// picos de requisições.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada com o rate limiter configurado.</returns>
    public static IServiceCollection AddLimiterRate(this IServiceCollection services)
    {

        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context => RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }
            ));

            options.AddFixedWindowLimiter("strict", opt =>
            {
                opt.PermitLimit = 10;
                opt.Window = TimeSpan.FromMinutes(1);
            });
        });

        return services;
    }

    /// <summary>
    /// Configura e registra o Swagger/OpenAPI para documentação da API.
    /// Adiciona o explorador de endpoints e o gerador de documentação, permitindo
    /// visualizar e testar os endpoints durante o desenvolvimento.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada com o Swagger configurado.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

}