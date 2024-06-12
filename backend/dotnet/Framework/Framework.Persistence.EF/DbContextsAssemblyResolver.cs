using System.Reflection;

namespace Framework.Persistence.EF;

/// <summary>
///     Resolves assemblies containing DbContext types.
/// </summary>
public class DbContextsAssemblyResolver : IDbContextsAssemblyResolver
{
    private readonly List<Assembly> _assemblies = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="DbContextsAssemblyResolver" /> class.
    /// </summary>
    /// <param name="dbContextTypes">The list of DbContext types whose assemblies need to be resolved.</param>
    public DbContextsAssemblyResolver(List<Type> dbContextTypes)
    {
        // Add the assemblies of DbContext types to the list of assemblies
        foreach (var context in dbContextTypes) _assemblies.Add(context.Assembly);
    }

    /// <summary>
    ///     Gets the list of assemblies containing DbContext types.
    /// </summary>
    /// <returns>The list of assemblies.</returns>
    public List<Assembly> GetAssemblies()
    {
        return _assemblies;
    }
}