using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.DataAccess.Context;
using Todo.DataAccess.Repositories;

namespace Todo.DataAccess.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<ITodoRepository, TodoRepository>();

        return services;
    }
}