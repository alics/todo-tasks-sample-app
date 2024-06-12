import { TaskRepository } from "src/domain/tasks/port/task.repository";
import { Inject } from '@nestjs/common';
import { ICommandHandler, CommandHandler } from '@nestjs/cqrs';
import { UpdateTaskCommand } from "../contracts/commands/tasks/update-task.command";
import { TaskNotFoundTitleException } from "../exceptions/task-notfound.exception";

/**
 * Handles the update of a task by processing the UpdateTaskCommand.
 */
@CommandHandler(UpdateTaskCommand)
export class UpdateTaskCommandHandler implements ICommandHandler<UpdateTaskCommand> {

    /**
     * Injects the TaskRepository instance.
     * 
     * @param taskRepository - The repository for task data operations.
     */
    constructor(@Inject(TaskRepository) private readonly taskRepository: TaskRepository) { }

    /**
     * Executes the task update logic.
     * 
     * @param command - The command containing the details of the task to be updated.
     * @returns A promise indicating the completion of the task update.
     * @throws TaskNotFoundTitleException - If the task with the specified ID is not found.
     */
    async execute(command: UpdateTaskCommand): Promise<any> 
    {
        const task = await this.taskRepository.getByIdAsync(command.id);

        if (task == null) {
            throw new TaskNotFoundTitleException(command.id);
        }

        task.updateTitle(command.title);
        task.updateStatus(command.status);
        task.updateDeadline(command.deadline);

        await this.taskRepository.updateAsync(task);
    }
}
