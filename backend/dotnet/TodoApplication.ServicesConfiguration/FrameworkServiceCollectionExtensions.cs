using Framework.Application;
using Framework.Core;
using Framework.Core.Queries;
using Framework.Persistence.EF;
using Framework.Snowflake;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApplication.ServicesConfiguration;

public static class FrameworkServiceCollectionExtensions
{
    public static IServiceCollection AddFrameworkServices(this IServiceCollection services)
    {
        services.AddScoped<ICommandBus, CommandBus>();
        services.AddScoped<IQueryBus, QueryBus>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IIdGenerator, SnowflakeIdGenerator>();

        return services;
    }
}