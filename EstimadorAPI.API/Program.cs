using EstimadorAPI.Application;
using EstimadorAPI.Infrastructure;
using FluentValidation;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de aplicación e infraestructura
var sqlServerConnection = builder.Configuration.GetConnectionString("SqlServer");
var sqliteConnection = builder.Configuration.GetConnectionString("SQLite");

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(sqlServerConnection, sqliteConnection);

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Agregar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Estimator API",
        Version = "v1.0",
        Description = "API para estimación de requisitos funcionales de casos de uso",
        Contact = new OpenApiContact
        {
            Name = "Estimator Team",
            Email = "estimator@example.com"
        }
    });

    // Incluir comentarios XML
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Estimator API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Crear e inicializar la base de datos
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EstimadorAPI.Infrastructure.Persistence.EstimadorDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();
