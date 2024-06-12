namespace Framework.Persistence.EF;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Represents a general-purpose DbContext used for interacting with the database.
/// </summary>
public class GeneralDbContext : DbContext
{
    private readonly IDbContextsAssemblyResolver _dbContextsAssemblyResolver;

    /// <summary>
    /// Initializes a new instance of the <see cref="GeneralDbContext"/> class.
    /// </summary>
    /// <param name="options">The options for configuring the context.</param>
    /// <param name="dbContextsAssemblyResolver">The resolver for DbContext assemblies.</param>
    public GeneralDbContext(DbContextOptions<GeneralDbContext> options,
                            IDbContextsAssemblyResolver dbContextsAssemblyResolver)
        : base(options)
    {
        _dbContextsAssemblyResolver = dbContextsAssemblyResolver;
    }

    /// <summary>
    /// Configures the model using the provided builder.
    /// </summary>
    /// <param name="modelBuilder">The builder to use for configuring the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations from assemblies containing DbContext types
        foreach (var assembly in _dbContextsAssemblyResolver.GetAssemblies())
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">Indicates whether the context should save all changes on success.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
                                                      CancellationToken cancellationToken = default)
    {
        // Call the base SaveChangesAsync method
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        return result;
    }
}