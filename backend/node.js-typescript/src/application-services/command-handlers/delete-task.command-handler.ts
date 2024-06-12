import { TaskRepository } from "src/domain/tasks/port/task.repository";
import { Inject } from '@nestjs/common';
import { ICommandHandler, CommandHandler } from '@nestjs/cqrs';
import { DeleteTaskCommand } from "../contracts/commands/tasks/delete-task.command";

/**
 * Handles the deletion of a task by processing the DeleteTaskCommand.
 */
@CommandHandler(DeleteTaskCommand)
export class DeleteTaskCommandHandler implements ICommandHandler<DeleteTaskCommand> {

    /**
     * Injects the TaskRepository instance.
     * 
     * @param taskRepository - The repository for task data operations.
     */
    constructor(@Inject(TaskRepository) private readonly taskRepository: TaskRepository) { }

    /**
     * Executes the task deletion logic.
     * 
     * @param command - The command containing the ID of the task to be deleted.
     * @returns A promise indicating the completion of the task deletion.
     */
    async execute(command: DeleteTaskCommand): Promise<any> {
        await this.taskRepository.deleteAsync(command.id);
    }
}
