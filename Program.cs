using CRUD_ASPNET.Repositories;
using CRUD_ASPNET.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

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

app.Run();