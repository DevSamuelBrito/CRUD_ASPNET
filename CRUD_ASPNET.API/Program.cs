using CRUD_ASPNET.API.Middleware;
using CRUD_ASPNET.Application.Services.Interfaces;
using CRUD_ASPNET.Configuration.Context;
using CRUD_ASPNET.Infra.Repositories.Interfaces;
using CRUD_ASPNET.Repositories;
using CRUD_ASPNET.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Configura logging para formato simples (evita duplicação visual)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

// Customiza respostas de validação
builder.Services.Configure<ApiBehaviorOptions>(options =>
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

//Rate limiting
builder.Services.AddRateLimiter(options =>
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

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//db context
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); sqlite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // postgresql


//Injection of dependencies
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

//CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

var swaggerSettings = builder.Configuration.GetSection("Swagger");
var routePrefix = swaggerSettings.GetValue<string>("RoutePrefix") ?? "api/docs";
var jsonRouteTemplate = swaggerSettings.GetValue<string>("JsonRouteTemplate");

if (app.Environment.IsDevelopment())
{

    if (!string.IsNullOrEmpty(jsonRouteTemplate))
    {
        app.UseSwagger(c => c.RouteTemplate = jsonRouteTemplate);

        var jsonEndpoint = "/" + jsonRouteTemplate.Replace("{documentName}", "v1");
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = routePrefix;
            options.SwaggerEndpoint(jsonEndpoint, "CRUD_ASPNET v1");
        });
    }
    else
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = routePrefix;
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD_ASPNET v1");
        });
    }

    // app.UseSwagger();
    // app.UseSwaggerUI(options =>
    // {
    //     options.RoutePrefix = "api/docs";
    //     options.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD_ASPNET v1");
    // });
}

app.UseHttpsRedirection();

app.UseCors();

app.UseRateLimiter();

app.MapControllers();

app.Run();

// dev: http://localhost:5272/api/docs/index.html