namespace Framework.Domain;

/// <summary>
///     Represents an abstract base class for entities in the domain model.
/// </summary>
/// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
public abstract class Entity<TKey>
{
    /// <summary>
    ///     Gets or protected sets the primary key of the entity.
    /// </summary>
    public TKey Id { get; protected set; }

    /// <summary>
    ///     Determines whether the specified object is equal to the current entity.
    /// </summary>
    /// <param name="obj">The object to compare with the current entity.</param>
    /// <returns>True if the specified object is equal to the current entity; otherwise, false.</returns>
    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var otherEntity = obj as Entity<TKey>;
        return Id.Equals(otherEntity.Id);
    }

    /// <summary>
    ///     Returns the hash code for the entity.
    /// </summary>
    /// <returns>A hash code for the entity.</returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}