import { TaskStatus } from "src/common/task-status";

/**
 * Represents the result of a task query.
 */
export class TaskQueryResult {
    id: string;
    title: string;
    creationDate: Date;
    deadline: Date;
    status: TaskStatus;

    /**
     * Constructs a new TaskQueryResult.
     * 
     * @param id - The unique identifier of the task.
     * @param title - The title of the task.
     * @param creationDate - The creation date of the task.
     * @param deadline - The deadline of the task.
     * @param status - The status of the task.
     */
    constructor(
        id: string,
        title: string,
        creationDate: Date,
        deadline: Date,
        status: TaskStatus
    ) {
        this.id = id;
        this.title = title;
        this.creationDate = creationDate;
        this.deadline = deadline;
        this.status = status;
    }
}
