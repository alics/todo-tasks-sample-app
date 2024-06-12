import { BaseEntity } from "typeorm/repository/BaseEntity";
import { AggregateRootImpl } from "../../../domain/aggregate-root.impl";

/**
 * Interface for mapping between domain entities and TypeORM entities.
 */
export interface TypeOrmEntityMapper<
    TKey,
    TDomainEntity extends AggregateRootImpl<TKey>,
    TTypeOrmEntity extends BaseEntity
> {
    /**
     * Maps a domain entity to a TypeORM entity.
     * 
     * @param domainEntity - The domain entity to map.
     * @returns The corresponding TypeORM entity.
     */
    toOrmEntity(domainEntity: TDomainEntity): TTypeOrmEntity;

    /**
     * Maps a TypeORM entity to a domain entity.
     * 
     * @param ormEntity - The TypeORM entity to map.
     * @returns The corresponding domain entity.
     */
    toDomainEntity(ormEntity: TTypeOrmEntity): TDomainEntity;
}
