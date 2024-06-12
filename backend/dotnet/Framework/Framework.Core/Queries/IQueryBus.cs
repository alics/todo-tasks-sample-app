namespace Framework.Core.Queries;

/// <summary>
///     Represents a query bus interface responsible for dispatching queries and returning query results asynchronously.
/// </summary>
public interface IQueryBus
{
    /// <summary>
    ///     Dispatches a query with a specified filter and returns the query result asynchronously.
    /// </summary>
    /// <typeparam name="TQueryFilter">The type of query filter.</typeparam>
    /// <typeparam name="TQueryResult">The type of query result.</typeparam>
    /// <param name="filter">The query filter to apply.</param>
    /// <returns>A task representing the asynchronous operation. The task result is the query result.</returns>
    /// <remarks>
    ///     Implementations of this method should handle the logic of executing the query based on the provided filter
    ///     and return the result asynchronously. The method should respect the types specified by the generic constraints.
    /// </remarks>
    Task<TQueryResult> Dispatch<TQueryFilter, TQueryResult>(TQueryFilter filter)
        where TQueryFilter : IQueryFilter
        where TQueryResult : IQueryResult;
}