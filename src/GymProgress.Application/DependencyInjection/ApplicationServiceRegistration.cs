using FluentValidation;
using GymProgress.Application.Interfaces.Services;
using GymProgress.Application.Services;
using GymProgress.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GymProgress.Application.DependencyInjection;

/// <summary>
/// A single entry point Program.cs can call to wire up everything this layer
/// owns, so the composition root doesn't need to know Application's internals.
/// </summary>
public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGymLogService, GymLogService>();
        services.AddScoped<IProfileService, ProfileService>();

        return services;
    }
}
