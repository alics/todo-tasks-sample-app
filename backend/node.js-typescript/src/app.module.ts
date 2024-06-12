import { Module } from '@nestjs/common';
import { TasksController } from './controllers/tasks.controller';
import { TasksApplicationServiceImpl } from './application-services/tasks.app.service.impl';
import { TasksApplicationService } from './application-services/ports/tasks.app.service';
import { TypeOrmModule } from '@nestjs/typeorm/dist/typeorm.module';
import { CqrsModule } from '@nestjs/cqrs';
import { ApplicationServiceModule } from './application-services/app.service.module';
import { APP_FILTER } from '@nestjs/core';
import { HttpExceptionFilter } from './controllers/filters/http-exception.filter';

@Module({
  imports: [TypeOrmModule.forRoot({
    type: 'mariadb',
    host: '127.0.0.1',
    port: 3306,
    database: 'todo',
    username: 'root',
    password: '8113024',
    entities: ['dist/**/*.entity{.ts,.js}'],
    synchronize: false,
  }), CqrsModule, ApplicationServiceModule],

  controllers: [TasksController],
  providers: [{
    provide: TasksApplicationService,
    useClass: TasksApplicationServiceImpl
  },{
    provide: APP_FILTER,
    useClass: HttpExceptionFilter,
  }
  ],
})
export class AppModule { }
