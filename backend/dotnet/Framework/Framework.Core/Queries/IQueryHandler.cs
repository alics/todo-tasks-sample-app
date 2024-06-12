namespace Framework.Core.Queries;

/// <summary>
///     Represents a generic interface for query handlers responsible for handling queries asynchronously.
/// </summary>
/// <typeparam name="TQueryFilter">The type of query filter.</typeparam>
/// <typeparam name="TQueryResult">The type of query result.</typeparam>
public interface IQueryHandler<in TQueryFilter, TQueryResult>
    where TQueryFilter : IQueryFilter
    where TQueryResult : IQueryResult
{
    /// <summary>
    ///     Handles a query asynchronously and returns the query result.
    /// </summary>
    /// <param name="filter">The query filter to apply.</param>
    /// <returns>A task representing the asynchronous operation. The task result is the query result.</returns>
    /// <remarks>
    ///     Implementations of this method should handle the logic of executing the query based on the provided filter
    ///     and return the result asynchronously. The method should respect the types specified by the generic constraints.
    /// </remarks>
    Task<TQueryResult> HandleAsync(TQueryFilter filter);
}