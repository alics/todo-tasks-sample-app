import { Injectable } from '@nestjs/common';
import { TasksApplicationService } from 'src/application-services/ports/tasks.app.service';
import { CreateTaskResponseDto } from './contracts/create-task-response.dto';
import { v4 as uuidv4 } from 'uuid';
import { CommandBus, QueryBus } from '@nestjs/cqrs';
import { CreateTaskCommand } from './contracts/commands/tasks/create-task.command';
import { TaskStatus } from 'src/common/task-status';
import { UpdateTaskCommand } from './contracts/commands/tasks/update-task.command';
import { DeleteTaskCommand } from './contracts/commands/tasks/delete-task.command';
import { TasksQueryFilter } from './contracts/queries/tasks/task.query.filter';
import { TaskQueryResult } from './contracts/queries/tasks/tasks.query.result';
import { CollectionQueryResult } from 'src/infrastructure/core/query-handler/collection.query.result';

/**
 * Implementation of the TasksApplicationService interface.
 */
@Injectable()
export class TasksApplicationServiceImpl implements TasksApplicationService {

  constructor(private readonly commandBus: CommandBus, private readonly queryBus: QueryBus) { }

  /**
   * Creates a new task asynchronously.
   * 
   * @param title - The title of the task to create.
   * @param deadline - The deadline of the task to create.
   * @returns A promise resolving to a CreateTaskResponseDto indicating the result of the creation operation.
   */
  async createTaskAsync(title: string, deadline: Date): Promise<CreateTaskResponseDto> {
    const taskId = uuidv4();
    const createTaskCommand = new CreateTaskCommand(taskId, title, deadline);
    await this.commandBus.execute(createTaskCommand);

    return new CreateTaskResponseDto(taskId, "The task was successfully created.");
  }

  /**
   * Updates an existing task asynchronously.
   * 
   * @param id - The identifier of the task to update.
   * @param title - The new title for the task.
   * @param deadline - The new deadline for the task.
   * @param status - The new status for the task.
   */
  async updateTaskAsync(id: string, title: string, deadline: Date, status: TaskStatus): Promise<void> {
    const updateTaskCommand = new UpdateTaskCommand(id, title, status, deadline);
    await this.commandBus.execute(updateTaskCommand);
  }

  /**
   * Deletes a task asynchronously.
   * 
   * @param id - The identifier of the task to delete.
   */
  async deleteTaskAsync(id: string): Promise<void> {
    const deleteTaskCommand = new DeleteTaskCommand(id);
    await this.commandBus.execute(deleteTaskCommand);
  }

  /**
   * Retrieves tasks asynchronously based on the provided filter criteria.
   * 
   * @param filter - The filter criteria for querying tasks.
   * @returns A promise resolving to a CollectionQueryResult containing the task query results.
   */
  async getTasksAsync(filter: TasksQueryFilter): Promise<CollectionQueryResult<TaskQueryResult>> {
    return await this.queryBus.execute(filter);
  }
}
