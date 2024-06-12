namespace Framework.Core;

/// <summary>
///     Represents an interface for generating long unique identifiers.
/// </summary>
public interface IIdGenerator
{
    /// <summary>
    ///     Generates a unique identifier.
    /// </summary>
    /// <returns>A long unique identifier.</returns>
    long Create();
}