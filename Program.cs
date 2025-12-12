using CRUD_ASPNET.Configuration.Context;
using CRUD_ASPNET.Repositories;
using CRUD_ASPNET.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//db context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


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

app.MapControllers();

app.Run();

// dev: http://localhost:5272/api/docs/index.html