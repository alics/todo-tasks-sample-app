/**
 * Base class for value objects.
 */
export abstract class ValueObject<TValueObject> {
    /**
     * Retrieves the components that contribute to the equality comparison of the value object.
     * @returns An array of objects representing the equality components.
     */
    protected abstract getEqualityComponents(): any[];

    /**
     * Determines whether the current value object is equal to another object.
     * @param obj The object to compare with the current value object.
     * @returns true if the specified object is equal to the current value object; otherwise, false.
     */
    equals(obj: any): boolean {
        // Check if the specified object is null or of a different type
        if (obj === null || obj.constructor !== this.constructor) {
            return false;
        }

        // Cast the object to the derived value object type
        const other = obj as ValueObject<TValueObject>;

        // Compare equality components using array equality (implementing sequence equality manually)
        const components = this.getEqualityComponents();
        const otherComponents = other.getEqualityComponents();

        if (components.length !== otherComponents.length) {
            return false;
        }

        for (let i = 0; i < components.length; i++) {
            if (components[i] !== otherComponents[i]) {
                return false;
            }
        }

        return true;
    }

    /**
     * Returns the hash code for the current value object.
     * @returns A 32-bit signed integer hash code.
     */
    getHashCode(): number {
        // Calculate the hash code based on the hash codes of equality components
        const components = this.getEqualityComponents();
        return components.reduce((x, y) => x ^ (y ? this.hash(y) : 0), 0);
    }

    /**
     * A simple hash function for an object.
     * @param obj The object to hash.
     * @returns A hash code for the object.
     */
    private hash(obj: any): number {
        if (typeof obj === "string") {
            let hash = 0;
            for (let i = 0; i < obj.length; i++) {
                const char = obj.charCodeAt(i);
                hash = ((hash << 5) - hash) + char;
                hash |= 0; // Convert to 32bit integer
            }
            return hash;
        } else if (typeof obj === "number") {
            return obj;
        } else {
            return JSON.stringify(obj).split("").reduce((a, b) => {
                a = ((a << 5) - a) + b.charCodeAt(0);
                return a & a;
            }, 0);
        }
    }
}
