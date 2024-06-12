using Microsoft.Extensions.DependencyInjection;

namespace Framework.Core.Queries;

/// <summary>
///     Represents a query bus implementation responsible for dispatching queries and returning query results
///     asynchronously.
/// </summary>
public class QueryBus : IQueryBus
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryBus" /> class with the specified service provider.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve query handlers.</param>
    public QueryBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ??
                           throw new ArgumentNullException(nameof(serviceProvider), "Service provider cannot be null.");
    }

    /// <summary>
    ///     Dispatches a query with a specified filter and returns the query result asynchronously.
    /// </summary>
    /// <typeparam name="TQueryFilter">The type of query filter.</typeparam>
    /// <typeparam name="TQueryResult">The type of query result.</typeparam>
    /// <param name="filter">The query filter to apply.</param>
    /// <returns>A task representing the asynchronous operation. The task result is the query result.</returns>
    /// <remarks>
    ///     This method resolves the appropriate query handler for the specified query filter and delegates
    ///     the handling of the query to the handler, returning the result asynchronously.
    /// </remarks>
    public async Task<TQueryResult> Dispatch<TQueryFilter, TQueryResult>(TQueryFilter filter)
        where TQueryFilter : IQueryFilter
        where TQueryResult : IQueryResult
    {
        var handler = _serviceProvider.GetService<IQueryHandler<TQueryFilter, TQueryResult>>()
                      ?? throw new InvalidOperationException(
                          $"No query handler registered for {typeof(IQueryHandler<TQueryFilter, TQueryResult>).Name}.");

        var result = await handler.HandleAsync(filter);
        return result;
    }
}