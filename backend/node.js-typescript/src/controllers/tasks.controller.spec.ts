import { Test, TestingModule } from '@nestjs/testing';
import { TasksController } from './tasks.controller';
import { TasksApplicationServiceImpl } from 'src/application-services/tasks.app.service.impl';

describe('TasksController', () => {
  let controller: TasksController;

  beforeEach(async () => {
    const app: TestingModule = await Test.createTestingModule({
      controllers: [TasksController],
      providers: [TasksApplicationServiceImpl],
    }).compile();

    controller = app.get<TasksController>(TasksController);
  });

  describe('root', () => {
    it('should return "Hello World!"', () => {
     // expect(controller.getHello()).toBe('Hello World!');
    });
  });
});
