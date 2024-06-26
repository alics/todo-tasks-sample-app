import { RouteDefinitionDto } from '../dto/route.dto';

export const ROUTE_DEFINITION: RouteDefinitionDto<string> = {
  APP: {
    TASKS: 'app.tasks',
    NOT_FOUND: 'app.not-found',
  },
  TASKS: {
    CREATE: 'tasks.create',
    EDIT: 'tasks.edit',
  },
};
