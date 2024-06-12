import { TaskStatus } from "src/common/task-status";
import { CreateTaskResponseDto } from "../contracts/create-task-response.dto";
import { TasksQueryFilter } from "../contracts/queries/tasks/task.query.filter";
import { TaskQueryResult } from "../contracts/queries/tasks/tasks.query.result";
import { CollectionQueryResult } from "src/infrastructure/core/query-handler/collection.query.result";

/**
* Interface for managing tasks in the application layer.
*/
export interface TasksApplicationService {
   /**
    * Creates a new task asynchronously.
    * @param title The title of the task.
    * @param deadlineDate The deadline date of the task.
    * @returns A promise representing the asynchronous operation. The promise result contains the response of the creation operation.
    */
   createTaskAsync(title: string, deadlineDate: Date): Promise<CreateTaskResponseDto>;

   /**
* Updates an existing task asynchronously.
* @param id The ID of the task to update.
* @param title The new title of the task.
* @param deadlineDate The new deadline date of the task.
* @param status The new status of the task.
* @returns A promise representing the asynchronous operation.
*/
   updateTaskAsync(id: string, title: string, deadlineDate: Date, status: TaskStatus): Promise<void>;

   /**
    * Retrieves tasks based on the specified filter asynchronously.
    * @param filter The filter criteria for retrieving tasks.
    * @returns A promise representing the asynchronous operation. The promise result contains the collection query result of todo tasks.
    */
   getTasksAsync(filter: TasksQueryFilter): Promise<CollectionQueryResult<TaskQueryResult>>;

   /**
    * Deletes a task asynchronously.
    * @param id The ID of the task to delete.
    * @returns A promise representing the asynchronous operation.
    */
   deleteTaskAsync(id: string): Promise<void>;
}

export const TasksApplicationService = Symbol("TasksApplicationService");