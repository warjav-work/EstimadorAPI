using EstimadorAPI.Application;
using EstimadorAPI.Infrastructure;
using Scalar.AspNetCore;

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
    app.MapScalarApiReference();
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
