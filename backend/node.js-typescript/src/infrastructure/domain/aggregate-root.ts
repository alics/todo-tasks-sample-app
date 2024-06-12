/**
 * Represents an interface for aggregate roots in the domain model.
 * Aggregate roots are entities that serve as the root of an aggregate,
 * representing a consistency boundary for a group of related objects that are
 * treated as a single unit during data transactions.
 */
export interface AggregateRoot {
    // This interface is empty and serves as a marker interface for aggregate roots.
}

export const AggregateRoot = Symbol("AggregateRoot");