import { Inject, Injectable } from '@nestjs/common';
import { InjectDataSource, InjectRepository } from '@nestjs/typeorm';
import { DataSource, Repository } from 'typeorm';
import { TaskRepository } from "src/domain/tasks/port/task.repository";
import { Task } from "src/domain/tasks/task";
import { TaskEntity } from './task.entity';
import { TaskMapper } from '../mapping/task.mapper';
import { TaskHistoryEntity } from './task-history.entity';

@Injectable()
export class TaskRepositoryImpl implements TaskRepository {

    /**
     * Initializes a new instance of the TaskRepositoryImpl class.
     * @param tasksRepository - The repository for tasks.
     * @param tasksHistoryRepository - The repository for task histories.
     * @param dataSource - The data source.
     * @param taskMapper - The task mapper.
     */
    constructor(
        @InjectRepository(TaskEntity) private tasksRepository: Repository<TaskEntity>,
        @InjectRepository(TaskHistoryEntity) private tasksHistoryRepository: Repository<TaskHistoryEntity>,
        @InjectDataSource() private dataSource: DataSource,
        @Inject(TaskMapper) private readonly taskMapper: TaskMapper
    ) { }

    /**
     * Updates a task asynchronously.
     * @param task - The task to update.
     * @returns A Promise representing the asynchronous operation completion.
     */
    async updateAsync(task: Task): Promise<void> {
        let taskEntity = this.taskMapper.toOrmEntity(task);

        const queryRunner = this.dataSource.createQueryRunner();
        await queryRunner.connect();
        await queryRunner.startTransaction();

        try {
            await this.tasksHistoryRepository.delete({ task: { id: task.id } });
            taskEntity.save();
        }

        catch (error) {
            // Rollback the transaction in case of an error
            await queryRunner.rollbackTransaction();
            throw error;
        } finally {
            // Release the query runner
            await queryRunner.release();
        }
    }

    /**
     * Deletes a task asynchronously by ID.
     * @param id - The ID of the task to delete.
     * @returns A Promise representing the asynchronous operation completion.
     */
    async deleteAsync(id: string): Promise<void> {
        await this.tasksRepository.delete(id);
    }

    /**
     * Retrieves a task asynchronously by ID.
     * @param id - The ID of the task to retrieve.
     * @returns A Promise that resolves to the retrieved task or null if not found.
     */
    async getByIdAsync(id: string): Promise<Task> {
        let taskEntity = await this.tasksRepository.findOne({ where: { id: id }, relations: ['histories'] });
        if (taskEntity)
            return this.taskMapper.toDomainEntity(taskEntity);

        return null;
    }

    /**
     * Creates a task asynchronously.
     * @param task - The task to create.
     * @returns A Promise representing the asynchronous operation completion.
     */
    async createAsync(task: Task): Promise<void> {
        const entity = this.taskMapper.toOrmEntity(task);
        await this.tasksRepository.save(entity);
    }
}
