using CRUD_ASPNET.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.ConfigurePipeline();

app.Run();

// dev: http://localhost:5272/api/docs/index.html