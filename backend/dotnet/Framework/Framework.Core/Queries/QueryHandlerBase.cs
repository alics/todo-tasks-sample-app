namespace Framework.Core.Queries;

/// <summary>
///     Represents a base abstract class for query handlers providing common functionality and structure.
/// </summary>
/// <typeparam name="TEntity">The type of entity the query handler operates on.</typeparam>
public abstract class QueryHandlerBase<TEntity>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryHandlerBase{TEntity}" /> class.
    /// </summary>
    protected QueryHandlerBase()
    {
    }
}