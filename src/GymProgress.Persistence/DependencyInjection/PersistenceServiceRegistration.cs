using GymProgress.Domain.Interfaces.Repositories;
using GymProgress.Persistence.Contexts;
using GymProgress.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymProgress.Persistence.DependencyInjection;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        // For SQL Server
        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseSqlServer(connectionString, sql =>
        //        sql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Npgsql (PostgreSQL)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, npgsql =>
                npgsql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IGymLogRepository, GymLogRepository>();
        services.AddScoped<IDayTypeRepository, DayTypeRepository>();
        services.AddScoped<IMuscleGroupRepository, MuscleGroupRepository>();

        return services;
    }
}
