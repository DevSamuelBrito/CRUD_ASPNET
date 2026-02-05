using CRUD_ASPNET.API.Extensions;
using CRUD_ASPNET.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigureLogging();

builder.Services.AddControllers();

// Customiza respostas de validação
builder.Services.CustomValidationResponses();

//Rate limiting
builder.Services.AddRateLimiter();

//swagger
builder.Services.AddSwagger();

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
app.ApplyMigrations();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseSwaggerFromConfiguration();

app.UseHttpsRedirection();

app.UseCors();

app.UseRateLimiter();

app.MapControllers();

app.Run();

// dev: http://localhost:5272/api/docs/index.html