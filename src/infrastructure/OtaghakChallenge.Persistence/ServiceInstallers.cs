using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OtaghakChallenge.Application.Repository;
using OtaghakChallenge.Persistence;

namespace OtaghakChallenge.Persistence.Infrastructure;

public static class ServiceInstallers
{
    //TODO: Move this method to framework

    public static void AddAppDBContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(optins => optins.UseSqlServer(configuration.GetConnectionString("Application")));

    }


    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        services.AddEntityFrameworkSqlServer()
        .AddDbContext<ApplicationDbContext>(options =>
        {
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
            options.UseSqlServer(configuration.GetConnectionString(environmentName));
            options.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
        });

        services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();

        return services;
    }


}