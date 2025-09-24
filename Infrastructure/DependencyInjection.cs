using Domain.DBContext;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection InjectInfra(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();
        return services;
    }

}
