using Framework.Core;
using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence.EF;

/// <summary>
/// Represents a unit of work for managing transactions and database operations.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UnitOfWork(DbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Saves changes made in the unit of work to the database asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Rolls back the current transaction asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task RollbackAsync()
    {
        if (context.Database.CurrentTransaction != null)
        {
            await context.Database.CurrentTransaction.RollbackAsync();
        }
    }
}