// Assuming TodoTaskStatus is an enum or similar construct in TypeScript

import { TaskStatus } from "src/common/task-status";
import { ValueObject } from "src/infrastructure/domain/value-object";

/**
 * Represents a historical record of a task's status change.
 */
export class TaskHistory extends ValueObject<TaskHistory> {

    public taskId: string;
    public taskStatus: TaskStatus;
    public dateTime: Date;
    public taskHistories: ReadonlyArray<TaskHistory>;

    /**
     * Initializes a new instance of the TaskHistory class.
     * @param taskId The ID of the task.
     * @param taskStatus The status of the task.
     */
    constructor(taskStatus: TaskStatus,dateTime: Date,taskId: string) {
        super();
        this.taskId = taskId;
        this.taskStatus = taskStatus;
        this.dateTime = dateTime;

    }

 

    /**
     * Retrieves the equality components used for comparison.
     * @returns An array of equality components.
     */
    protected getEqualityComponents(): any[] {
        return [this.taskId, this.taskStatus];
    }
}
