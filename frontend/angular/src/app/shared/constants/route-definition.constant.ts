import { RouteDefinitionDto } from '../dto/route.dto';

// Defining a constant ROUTE_DEFINITION of type RouteDefinitionDto<string>
export const ROUTE_DEFINITION: RouteDefinitionDto<string> = {
  APP: {
    TASKS: 'app.tasks', // Route path for tasks under the 'APP' section
    NOT_FOUND: 'app.not-found', // Route path for not-found under the 'APP' section
  },
  TASKS: {
    CREATE: 'tasks.create', // Route path for creating tasks under the 'TASKS' section
    EDIT: 'tasks.edit', // Route path for editing tasks under the 'TASKS' section
  },
};
