using Framework.Core.Queries;
using Microsoft.Extensions.DependencyInjection;
using TodoApplication.ApplicationService.QueryHandlers.TodoTask;
using TodoApplication.ReadModel.Contracts;

namespace TodoApplication.ServicesConfiguration;

public static class ReadModelServiceCollectionExtensions
{
    public static IServiceCollection AddReadModelServices(this IServiceCollection services)
    {
        services
            .AddTransient<IQueryHandler<TodoTasksQueryFilter, CollectionQueryResult<TodoTaskQueryResult>>,
                TodoTaskQueryHandler>();
        return services;
    }
}