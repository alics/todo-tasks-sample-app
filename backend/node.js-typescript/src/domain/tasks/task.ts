import { TaskStatus } from "src/common/task-status";
import { AggregateRootImpl } from "src/infrastructure/domain/aggregate-root.impl";
import { InvalidTaskTitleException } from "./exceptions/invalid-task.title.exception";
import { InvalidTaskDeadlineException } from "./exceptions/invalid-task-deadline.exception";
import { TaskHistory } from "./task-history";


export class Task extends AggregateRootImpl<string> {

    public id: string;
    public title: string;
    public creationDate: Date;
    public deadline: Date;
    public status: TaskStatus;
    public taskHistories: TaskHistory[];

    constructor(id: string, title: string, status: TaskStatus, deadline: Date, creationTime: Date) {
        super(id);
        this.id = id;
        this.creationDate = creationTime;
        this.status = status;
        this.updateTitle(title);
        this.updateDeadline(deadline);
    }

    /**
    * Initializes a new instance of the TodoTask class, optionally with specified id, title, and deadline.
    * @param id Optional. The unique identifier of the task.
    * @param title Optional. The title of the task.
    * @param deadline Optional. The deadline of the task.
    */
    public static createTask(id: string, title: string, deadline: Date): Task {
        const task = new Task(id, title, TaskStatus.Created, deadline, new Date);
        task.taskHistories=[];
        task.taskHistories.push(new TaskHistory(TaskStatus.Created, new Date(), id));
        return task;
    }

    /**
     * Updates the title of the task.
     * @param title The new title to be set.
     * Throws InvalidTaskTitleException if the title is null or less than 11 characters.
     */
    public updateTitle(title: string): void {
        if (!title || title.length < 11) {
            throw new InvalidTaskTitleException();
        }
        this.title = title;
    }

    /**
     * Updates the deadline of the task to a new date.
     * @param deadline The new deadline date.
     * Throws InvalidTaskDeadlineException if the deadline is not a future date.
     */
    public updateDeadline(deadline: Date): void {
        const today = new Date(new Date().toDateString()); // Normalize today's date
        if (deadline <= today) {
            throw new InvalidTaskDeadlineException();
        }
        this.deadline = deadline;
    }

    /**
     * Updates the status of the task to a new status.
     * @param status The new status to be set for the task.
     * Adds a new TaskHistory record when the status changes.
     */
    public updateStatus(status: TaskStatus): void {
        if (this.status !== status) {
            this.status = status;
            this.taskHistories.push(new TaskHistory(status, new Date(), this.id));
        }
    }
}