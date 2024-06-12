import { TaskStatus } from "src/common/task-status";

/**
 * Filter criteria for querying tasks.
 */
export class TasksQueryFilter {
    id?: string;
    title?: string;
    status?: TaskStatus;

    /**
     * Constructs a new TasksQueryFilter.
     * 
     * @param id - The unique identifier to filter tasks by.
     * @param title - The title to filter tasks by.
     * @param status - The status to filter tasks by.
     */
    constructor(id?: string, title?: string, status?: TaskStatus) {
        this.id = id;
        this.title = title;
        this.status = status;
    }
}
