/**
 * Command to delete an existing task.
 */
export class DeleteTaskCommand {
    id: string;

    /**
     * Constructs a new DeleteTaskCommand.
     * 
     * @param id - The unique identifier of the task to be deleted.
     */
    constructor(id: string) {
        this.id = id;
    }
}
