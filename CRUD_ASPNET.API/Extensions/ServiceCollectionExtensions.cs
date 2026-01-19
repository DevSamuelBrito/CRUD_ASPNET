using CRUD_ASPNET.Configuration.Context;
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
}
