import { IQueryHandler, QueryHandler } from '@nestjs/cqrs';
import { TasksQueryFilter } from '../contracts/queries/tasks/task.query.filter';
import { TaskQueryResult } from '../contracts/queries/tasks/tasks.query.result';
import { Repository } from 'typeorm/repository/Repository';
import { InjectRepository } from '@nestjs/typeorm';
import { TaskEntity } from 'src/persistence/typeorm/tasks/task.entity';
import { CollectionQueryResult } from 'src/infrastructure/core/query-handler/collection.query.result';

/**
 * Handles querying tasks based on filter criteria.
 */
@QueryHandler(TasksQueryFilter)
export class TasksQueryHandler implements IQueryHandler<TasksQueryFilter> {
    constructor(@InjectRepository(TaskEntity) private tasksRepository: Repository<TaskEntity>) { }

    /**
     * Executes the task query based on the provided filter criteria.
     * 
     * @param queryFilter - The filter criteria for querying tasks.
     * @returns A promise resolving to a collection of task query results.
     */
    async execute(queryFilter: TasksQueryFilter): Promise<CollectionQueryResult<TaskQueryResult>> {

        let tasksResult: Array<TaskQueryResult> = [];
        let queryBuilder = this.tasksRepository.createQueryBuilder('task');

        if (queryFilter.id) {
            queryBuilder = queryBuilder.where({ id: queryFilter.id });
        } else {
            if (queryFilter.title) {
                queryBuilder = queryBuilder.andWhere('task.title LIKE :title', { title: `%${queryFilter.title}%` });
            }

            if (queryFilter.status) {
                queryBuilder = queryBuilder.andWhere('status = :status', { status: queryFilter.status });
            }
        }

        const result = await queryBuilder.getMany();

        tasksResult = result.map(task => new TaskQueryResult(
            task.id,
            task.title,
            task.creationDate,
            task.deadline,
            task.status,
        ));

        return new CollectionQueryResult<TaskQueryResult>(tasksResult, tasksResult.length);
    }
}
