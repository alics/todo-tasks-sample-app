/**
 * Command to create a new task.
 */
export class CreateTaskCommand {
    id: string;
    title: string;
    deadline: Date;

    /**
     * Constructs a new CreateTaskCommand.
     * 
     * @param id - The unique identifier for the task.
     * @param title - The title of the task.
     * @param deadline - The deadline for the task.
     */
    constructor(id: string, title: string, deadline: Date) {
        this.id = id;
        this.title = title;
        this.deadline = deadline;
    }
}
