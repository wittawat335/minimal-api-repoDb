using Domain.DBContext;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection InjectInfra(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        services.AddTransient(typeof(IDapperRepository<>), typeof(DapperRepository<>));

        return services;
    }

}
