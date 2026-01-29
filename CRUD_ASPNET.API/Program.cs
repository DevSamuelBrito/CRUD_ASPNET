using CRUD_ASPNET.API.Extensions;
using CRUD_ASPNET.API.Middleware;
using CRUD_ASPNET.Configuration.Context;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigureLogging();

builder.Services.AddControllers();

// Customiza respostas de validação
builder.Services.CustomValidationResponses();

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

//db
builder.Services.AddDatabaseService(builder.Configuration);

//Injection of dependencies
builder.Services.AddScopedServices();

//CORS
builder.Services.AddCorsPolicy();

var app = builder.Build();

// Aplica migrations automaticamente
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

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