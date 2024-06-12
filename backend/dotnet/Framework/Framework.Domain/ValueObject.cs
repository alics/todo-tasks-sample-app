namespace Framework.Domain;

/// <summary>
///     Base class for value objects.
/// </summary>
/// <typeparam name="TValueObject">The type of the derived value object.</typeparam>
public abstract class ValueObject<TValueObject>
{
    /// <summary>
    ///     Retrieves the components that contribute to the equality comparison of the value object.
    /// </summary>
    /// <returns>An enumerable collection of objects representing the equality components.</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    ///     Determines whether the current value object is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with the current value object.</param>
    /// <returns>true if the specified object is equal to the current value object; otherwise, false.</returns>
    public override bool Equals(object obj)
    {
        // Check if the specified object is null or of a different type
        if (ReferenceEquals(obj, null) || obj.GetType() != GetType()) return false;

        // Cast the object to the derived value object type
        var other = (ValueObject<TValueObject>)obj;

        // Compare equality components using sequence equality
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    ///     Returns the hash code for the current value object.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    {
        // Calculate the hash code based on the hash codes of equality components
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
}