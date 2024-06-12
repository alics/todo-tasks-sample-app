/**
 * Represents a base class for application-specific exceptions.
 * This class extends JavaScript's built-in Error class.
 */
export abstract class ApplicationException extends Error {
    public statusCode: number;

    /**
     * Initializes a new instance of the BaseApplicationException class.
     * @param message Optional. A message that describes the error.
     */
    protected constructor(message?: string) {
        super(message);
        this.name = this.constructor.name;
        this.statusCode = 500; // Default status code
        Object.setPrototypeOf(this, new.target.prototype); // Proper prototype chain setup for extending built-in Error
    }

    /**
     * Gets the HTTP status code associated with the exception.
     * The default status code is 500, representing an internal server error.
     */
    public getStatusCode(): number {
        return this.statusCode;
    }
}
