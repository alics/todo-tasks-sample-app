import { ApplicationException } from "src/infrastructure/core/exceptions/application.execption";

/**
 *  Exception thrown when a task is not found.
 */
export class TaskNotFoundTitleException extends ApplicationException {
    /**
     * Initializes a new instance of the InvalidTaskTitleException class.
     */
    constructor(id:string) {
        super( `Task with id ${ id } was not found`);
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
