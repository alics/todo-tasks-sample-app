import { TaskStatus } from "src/common/task-status";

/**
 * Command to update an existing task.
 */
export class UpdateTaskCommand {
    id: string;
    title: string;
    status: TaskStatus;
    deadline: Date;

    /**
     * Constructs a new UpdateTaskCommand.
     * 
     * @param id - The unique identifier of the task to be updated.
     * @param title - The new title of the task.
     * @param status - The new status of the task.
     * @param deadline - The new deadline for the task.
     */
    constructor(id: string, title: string, status: TaskStatus, deadline: Date) {
        this.id = id;
        this.title = title;
        this.status = status;
        this.deadline = deadline;
    }
}
