using Microsoft.Extensions.DependencyInjection;
using Todo.Business.Services;

namespace Todo.Business.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();

        return services;
    }
}