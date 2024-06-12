import { Injectable } from '@nestjs/common';
import { Task } from "src/domain/tasks/task";
import { TypeOrmEntityMapper } from "src/infrastructure/core/persistence/typeorm/entity.mapper";
import { TaskEntity } from "../tasks/task.entity";
import { TaskHistoryEntity } from '../tasks/task-history.entity';
import { TaskHistory } from 'src/domain/tasks/task-history';

@Injectable()
export class TaskMapper implements TypeOrmEntityMapper<string, Task, TaskEntity> {
    /**
     * Maps a domain entity to a TypeORM entity.
     * @param domainEntity - The domain entity to map.
     * @returns The corresponding TypeORM entity.
     */
    toOrmEntity(domainEntity: Task): TaskEntity {
        let taskEntity = new TaskEntity(domainEntity.id, domainEntity.title, domainEntity.status, domainEntity.deadline, domainEntity.creationDate)
        taskEntity.histories = domainEntity.taskHistories.map(history =>
            new TaskHistoryEntity(history.taskStatus, history.dateTime, taskEntity)
        );

        return taskEntity;
    }

    /**
     * Maps a TypeORM entity to a domain entity.
     * @param ormEntity - The TypeORM entity to map.
     * @returns The corresponding domain entity.
     */
    toDomainEntity(ormEntity: TaskEntity): Task {
        const task = new Task(
            ormEntity.id,
            ormEntity.title,
            ormEntity.status,
            ormEntity.deadline,
            ormEntity.creationDate
        );

        task.taskHistories = ormEntity.histories.map(history =>
            new TaskHistory(history.status, history.dateTime, ormEntity.id)
        );

        return task;
    }
}
