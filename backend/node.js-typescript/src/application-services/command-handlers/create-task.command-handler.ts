import { CreateTaskCommand } from "../contracts/commands/tasks/create-task.command";
import { TaskRepository } from "src/domain/tasks/port/task.repository";
import { Task } from "src/domain/tasks/task";
import { Inject } from '@nestjs/common';
import { ICommandHandler, CommandHandler } from '@nestjs/cqrs';

/**
 * Handles the creation of a new task by processing the CreateTaskCommand.
 */
@CommandHandler(CreateTaskCommand)
export class CreateTaskCommandHandler implements ICommandHandler<CreateTaskCommand> {

    /**
     * Injects the TaskRepository instance.
     * 
     * @param taskRepository - The repository for task data operations.
     */
    constructor(@Inject(TaskRepository) private readonly taskRepository: TaskRepository) { }

    /**
     * Executes the task creation logic.
     * 
     * @param command - The command containing task creation details (id, title, deadline).
     * @returns A promise indicating the completion of the task creation.
     */
    async execute(command: CreateTaskCommand): Promise<any> {
        const task = Task.createTask(command.id, command.title, command.deadline);
        await this.taskRepository.createAsync(task);
    }
}
