import { Body, Controller, Delete, Get, HttpCode, HttpStatus, Inject, Param, Patch, Post, Put, Query, UseFilters } from '@nestjs/common';
import { TasksApplicationService } from 'src/application-services/ports/tasks.app.service';
import { CreateTaskDto } from './dto/create-task.dto';
import { ApiQuery, ApiTags } from '@nestjs/swagger';
import { UpdateTaskDto } from './dto/update-task.dto';
import { HttpExceptionFilter } from './filters/http-exception.filter';
import { TasksQueryFilter } from 'src/application-services/contracts/queries/tasks/task.query.filter';
import { TaskStatus } from 'src/common/task-status';

/**
 * Controller for managing tasks.
 */
@ApiTags('api/tasks')
@Controller('api/tasks')
@UseFilters(new HttpExceptionFilter())
export class TasksController {
  constructor(@Inject(TasksApplicationService) private readonly appService: TasksApplicationService) { }

  /**
   * Endpoint for creating a new task.
   * 
   * @param taskDto - The data transfer object containing task information.
   * @returns The result of the task creation operation.
   */
  @Post()
  @HttpCode(HttpStatus.CREATED)
  async createTask(@Body() taskDto: CreateTaskDto) {
    return await this.appService.createTaskAsync(taskDto.title, taskDto.deadline);
  }

  /**
   * Endpoint for retrieving a task by ID.
   * 
   * @param id - The ID of the task to retrieve.
   * @returns The retrieved task.
   */
  @Get(':id')
  @HttpCode(HttpStatus.OK)
  async getTask(@Param('id') id: string) {
    const filter = new TasksQueryFilter(id);

    var tasks = await this.appService.getTasksAsync(filter);
    if (tasks && tasks.items) {
      var result = tasks.items[0];
      return result;
    }
    return null;
  }

  /**
   * Endpoint for retrieving tasks based on optional filter criteria.
   * 
   * @param status - The status of the tasks to filter by.
   * @param title - The title of the tasks to filter by.
   * @returns The filtered tasks.
   */
  @Get()
  @HttpCode(HttpStatus.OK)
  @ApiQuery({ name: 'title', required: false, type: String, description: 'The title of the task' })
  @ApiQuery({ name: 'status', required: false, type: Number, description: 'The current status of the task' })
  async getTasks(@Query('status') status?: number, @Query('title') title?: string) {
    var taskStatus = null;
    if (status) {
      taskStatus = status as TaskStatus;
    }
    const filter = new TasksQueryFilter(null, title, taskStatus);

    var tasks = await this.appService.getTasksAsync(filter);
    return tasks;
  }

  /**
   * Endpoint for updating an existing task.
   * 
   * @param taskDto - The data transfer object containing updated task information.
   * @param id - The ID of the task to update.
   * @returns The result of the task update operation.
   */
  @Put(':id')
  @HttpCode(HttpStatus.OK)
  @HttpCode(HttpStatus.BAD_REQUEST)
  async updateTask(@Body() taskDto: UpdateTaskDto, @Param('id') id: string) {
    return await this.appService.updateTaskAsync(id, taskDto.title, taskDto.deadline, taskDto.status);
  }

  /**
   * Endpoint for deleting a task.
   * 
   * @param id - The ID of the task to delete.
   * @returns The result of the task deletion operation.
   */
  @Delete(':id')
  @HttpCode(HttpStatus.OK)
  async deleteTask(@Param('id') id: string) {
    return await this.appService.deleteTaskAsync(id);
  }
}
