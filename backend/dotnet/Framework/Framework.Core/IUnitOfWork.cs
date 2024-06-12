namespace Framework.Core;

/// <summary>
/// Represents an interface for a unit of work pattern used to manage transactions and database changes.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Asynchronously saves changes made in the unit of work to the underlying data store.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveChangesAsync();

    /// <summary>
    /// Asynchronously rolls back changes made in the unit of work.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RollbackAsync();
}
