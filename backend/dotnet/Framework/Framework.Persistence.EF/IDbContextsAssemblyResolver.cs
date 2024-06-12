using System.Reflection;

namespace Framework.Persistence.EF;

/// <summary>
/// Represents a resolver for assemblies containing DbContext types.
/// </summary>
public interface IDbContextsAssemblyResolver
{
    /// <summary>
    /// Retrieves the list of assemblies containing DbContext types.
    /// </summary>
    /// <returns>The list of assemblies.</returns>
    List<Assembly> GetAssemblies();
}