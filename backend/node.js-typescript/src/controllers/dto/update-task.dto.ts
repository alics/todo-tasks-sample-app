import { TaskStatus } from "src/common/task-status";
import { CreateTaskDto } from "./create-task.dto";
import { ApiProperty } from '@nestjs/swagger';

/**
 * Data transfer object (DTO) for updating a task.
 * Inherits properties from CreateTaskDto.
 */
export class UpdateTaskDto extends CreateTaskDto {
    @ApiProperty({ example: TaskStatus.Created, description: 'The current status of the task' })
    status: TaskStatus;

    /**
     * Constructs a new UpdateTaskDto.
     * 
     * @param title - The title of the task.
     * @param deadlineDate - The deadline date of the task.
     * @param status - The current status of the task.
     */
    constructor(title: string, deadlineDate: Date, status: TaskStatus) {
        super(title, deadlineDate);
        this.status = status;
    }
}
