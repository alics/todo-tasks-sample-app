namespace Framework.Domain
{
    /// <summary>
    /// Represents an interface for aggregate roots in the domain model.
    /// </summary>
    public interface IAggregateRoot
    {
        // This interface does not contain any members as it serves as a marker interface for aggregate roots.
        // Aggregate roots are entities that serve as the root of an aggregate, representing a consistency boundary
        // for a group of related objects that are treated as a single unit during data transactions.
    }

}
