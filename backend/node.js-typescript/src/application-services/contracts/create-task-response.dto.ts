/**
 * Data transfer object (DTO) for creating a task response.
 */
export class CreateTaskResponseDto {
    taskId: string;
    message: string;

    /**
     * Constructs a new CreateTaskResponseDto.
     * 
     * @param taskId - The identifier of the created task.
     * @param message - A message indicating the result of the creation operation.
     */
    constructor(taskId: string, message: string) {
        this.taskId = taskId;
        this.message = message;
    }
}
