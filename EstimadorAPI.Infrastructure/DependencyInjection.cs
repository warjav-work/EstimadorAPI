using EstimadorAPI.Domain.Interfaces.Repositories;
using EstimadorAPI.Domain.Interfaces.Services;
using EstimadorAPI.Infrastructure.Persistence;
using EstimadorAPI.Infrastructure.Persistence.Repositories;
using EstimadorAPI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EstimadorAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        string? sqlServerConnection = null,
        string? sqliteConnection = null)
    {
        if (!string.IsNullOrEmpty(sqlServerConnection))
        {
            // Usar SQL Server
            services.AddDbContext<EstimadorDbContext>(options =>
                options.UseSqlServer(sqlServerConnection,
                    b => b.MigrationsAssembly("EstimadorAPI.Infrastructure"))
            );
        }
        else if (!string.IsNullOrEmpty(sqliteConnection))
        {
            // Usar SQLite
            services.AddDbContext<EstimadorDbContext>(options =>
                options.UseSqlite(sqliteConnection,
                    b => b.MigrationsAssembly("EstimadorAPI.Infrastructure"))
            );
        }
        else
        {
            // Valor por defecto: SQLite en memoria
            services.AddDbContext<EstimadorDbContext>(options =>
                options.UseInMemoryDatabase("EstimadorDb")
            );
        }

        // Registrar Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Registrar servicios de dominio
        services.AddScoped<IEstimationService, EstimationService>();

        return services;
    }
}
