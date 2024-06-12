namespace Framework.Domain
{
    /// <summary>
    /// Represents an abstract base class for aggregate roots in the domain model.
    /// </summary>
    /// <typeparam name="TKey">The type of the aggregate root's primary key.</typeparam>
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    {

    }

}
