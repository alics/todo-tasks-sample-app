import { ApiProperty } from "@nestjs/swagger";
import { IsNumber, IsOptional, IsString } from "class-validator";

/**
 * Data transfer object (DTO) for querying tasks.
 */
export class TasksQueryDto {
    @IsString()
    @IsOptional()
    @ApiProperty({ description: 'The title of the task', required: false })
    title?: string;

    @IsNumber()
    @IsOptional()
    @ApiProperty({ description: 'The current status of the task', required: false, type: Number })
    status?: Number;
}