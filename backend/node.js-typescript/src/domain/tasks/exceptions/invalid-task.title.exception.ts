import { ApplicationException } from "src/infrastructure/core/exceptions/application.execption";

/**
 * Exception thrown when a task title is invalid.
 * This class extends BaseApplicationException and customizes the HTTP status code.
 */
export class InvalidTaskTitleException extends ApplicationException {
    /**
     * Initializes a new instance of the InvalidTaskTitleException class.
     */
    constructor() {
        super("Invalid task title. Title must be at least 11 characters long.");
        this.statusCode = 400; // Custom status code for invalid task title
    }

    /**
     * Gets the HTTP status code associated with this exception.
     * Overrides the default status code to provide a specific status code (400) for this exception.
     */
    public getStatusCode(): number {
        return this.statusCode;
    }
}
