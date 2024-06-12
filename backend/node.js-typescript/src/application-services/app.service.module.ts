import { Module } from '@nestjs/common';

import { CqrsModule } from '@nestjs/cqrs';
import { PersistenceModule } from 'src/persistence/typeorm/persistence.module';
import { CreateTaskCommandHandler } from './command-handlers/create-task.command-handler';
import { UpdateTaskCommandHandler } from './command-handlers/update-task.command-handler';
import { DeleteTaskCommandHandler } from './command-handlers/delete-task.command-handler';
import { TasksQueryHandler } from './query-handlers/tasks.query-handler';

@Module({
    imports: [CqrsModule, PersistenceModule],
    providers: [CreateTaskCommandHandler,UpdateTaskCommandHandler,DeleteTaskCommandHandler,TasksQueryHandler],

})
export class ApplicationServiceModule { }