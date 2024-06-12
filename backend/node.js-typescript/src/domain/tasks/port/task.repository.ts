import { Task } from "../task";

/**
 * Interface for a task repository.
 */
export interface TaskRepository {
    /**
     * Creates a task asynchronously.
     * 
     * @param task - The task to create.
     */
    createAsync(task: Task): Promise<void>;

    /**
     * Updates a task asynchronously.
     * 
     * @param task - The task to update.
     */
    updateAsync(task: Task): Promise<void>;

    /**
     * Deletes a task asynchronously by ID.
     * 
     * @param id - The ID of the task to delete.
     */
    deleteAsync(id: string): Promise<void>;

    /**
     * Retrieves a task asynchronously by ID.
     * 
     * @param id - The ID of the task to retrieve.
     * @returns The retrieved task, or null if not found.
     */
    getByIdAsync(id: string): Promise<Task | null>;
}

/**
 * Symbol used for dependency injection of TaskRepository.
 */
export const TaskRepository = Symbol("TaskRepository");
