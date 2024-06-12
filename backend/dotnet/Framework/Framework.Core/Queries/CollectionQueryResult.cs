namespace Framework.Core.Queries;

/// <summary>
///     Represents a query result containing a collection of items of type T.
/// </summary>
/// <typeparam name="T">The type of items contained in the collection.</typeparam>
public class CollectionQueryResult<T> : IQueryResult
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CollectionQueryResult{T}" /> class.
    /// </summary>
    /// <param name="items">The collection of items.</param>
    /// <param name="totalItems">The total number of items in the collection.</param>
    public CollectionQueryResult(IEnumerable<T> items, int totalItems)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items), "Items collection cannot be null.");
        TotalItems = totalItems;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CollectionQueryResult{T}" /> class.
    /// </summary>
    /// <param name="items">The collection of items.</param>
    public CollectionQueryResult(IEnumerable<T> items) : this(items, items.Count())
    {
    }

    /// <summary>
    ///     Gets or sets the total number of items in the collection.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    ///     Gets or sets the collection of items.
    /// </summary>
    public IEnumerable<T> Items { get; set; }
}