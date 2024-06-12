/**
 * Represents a query result containing a collection of items of type T.
 * @typeparam T - The type of items contained in the collection.
 */
export class CollectionQueryResult<T> {
    /**
     * The collection of items.
     */
    items: T[];

    /**
     * The total number of items in the collection.
     */
    totalItems: number;

    /**
     * Initializes a new instance of the CollectionQueryResult class.
     * @param items - The collection of items.
     * @param totalItems - The total number of items in the collection.
     */
    constructor(items: T[], totalItems?: number) {
        if (items == null) {
            throw new Error("Items collection cannot be null.");
        }

        this.items = items;
        this.totalItems = totalItems !== undefined ? totalItems : items.length;
    }
}
