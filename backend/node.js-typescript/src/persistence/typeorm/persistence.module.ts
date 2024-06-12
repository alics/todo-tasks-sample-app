import { Module } from '@nestjs/common';

import { TypeOrmModule } from '@nestjs/typeorm';
import { TaskEntity } from './tasks/task.entity';
import { TaskRepositoryImpl } from './tasks/task.repository.impl';
import { TaskRepository } from 'src/domain/tasks/port/task.repository';
import { TaskHistoryEntity } from './tasks/task-history.entity';
import { TaskMapper } from './mapping/task.mapper';
import { Repository } from 'typeorm';
 

@Module({
    imports: [TypeOrmModule.forFeature([TaskEntity,TaskHistoryEntity])],
    providers: [TaskMapper,Repository<TaskEntity>,{
        provide: TaskRepository,
        useClass: TaskRepositoryImpl,
    }],
    exports: [TaskRepository,Repository<TaskEntity>,TypeOrmModule.forFeature([TaskEntity])]

})
export class PersistenceModule { }
