import { IsNotEmpty } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

/**
 * Data transfer object (DTO) for creating a task.
 */
export class CreateTaskDto {
    @IsNotEmpty()
    @ApiProperty({ example: 'typescript ddd sample project', description: 'The title of the task' })
    title: string;

    @IsNotEmpty()
    @ApiProperty({ example: '2025-01-01', description: 'The deadline of the completion of the task' })
    deadline: Date;

    /**
     * Constructs a new CreateTaskDto.
     * 
     * @param title - The title of the task.
     * @param deadline - The deadline of the completion of the task.
     */
    constructor(title: string, deadline: Date) {
        this.title = title;
        this.deadline = deadline;
    }
}
