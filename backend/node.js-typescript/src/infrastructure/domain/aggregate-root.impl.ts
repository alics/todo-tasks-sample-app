import { AggregateRoot } from "./aggregate-root";
import { DomainEntity } from "./entity";


/**
 * Represents an abstract base class for aggregate roots in the domain model.
 */
export abstract class AggregateRootImpl<TKey> extends DomainEntity<TKey> implements AggregateRoot {
    // Currently, this class does not add any additional properties or methods beyond what Entity<TKey> provides.
    // You can add aggregate-specific logic here, such as version handling or domain events integration.
}