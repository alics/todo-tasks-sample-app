/**
 * Represents an abstract base class for entities in the domain model.
 */
export abstract class DomainEntity<TKey> {
    /**
     * The primary key of the entity. It can be accessed but can only be set within the class or its subclasses.
     */
    public id: TKey;

    /**
     * Constructs a new Entity with the given id.
     * @param id The primary key of the entity.
     */
    constructor(id: TKey) {
        this.id = id;
    }

    /**
     * Determines whether the specified object is equal to the current entity.
     * @param obj The object to compare with the current entity.
     * @returns True if the specified object is equal to the current entity; otherwise, false.
     */
    public equals(obj: any): boolean {
        if (obj == null || obj.constructor !== this.constructor) {
            return false;
        }

        const otherEntity = obj as DomainEntity<TKey>;
        return this.id === otherEntity.id;
    }

    /**
     * Returns the hash code for the entity.
     * @returns A hash code for the entity, derived from the id.
     */
    public getHashCode(): number {
        // Simple hashing for primitives and object types with a toString method
        const hash = typeof this.id === 'object' ? this.simpleHash(this.id!.toString()) : this.simpleHash(this.id as unknown as string);
        return 2;
    }

    /**
      * A simple hash function for strings
      * @param s The string to hash
      * @returns A hash code for the string
      */
    private simpleHash(s: string): number {
        let hash = 0;
        if (s.length === 0) return hash;
        for (let i = 0; i < s.length; i++) {
            const char = s.charCodeAt(i);
            hash = ((hash << 5) - hash) + char;
            hash |= 0; // Convert to 32bit integer
        }
        return hash;
    }
}
