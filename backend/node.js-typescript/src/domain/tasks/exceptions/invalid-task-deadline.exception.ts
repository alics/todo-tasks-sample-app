import { ApplicationException } from "src/infrastructure/core/exceptions/application.execption";

/**
 * Exception thrown when a task deadline is invalid.
 * This class extends BaseApplicationException and customizes the HTTP status code.
 */
export class InvalidTaskDeadlineException extends ApplicationException {
    /**
     * Initializes a new instance of the InvalidTaskDeadlineException class.
     */
    constructor() {
        super("Invalid task deadline. Deadline must be set to a future date.");
        this.statusCode = 400; // Custom status code for invalid task deadline
    }

    /**
     * Gets the HTTP status code associated with this exception.
     * Overrides the default status code to provide a specific status code (400) for this exception.
     */
    public getStatusCode(): number {
        return this.statusCode;
    }
}
