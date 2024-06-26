interface APP<T> {
  TASKS: T;
  NOT_FOUND: T;
}

interface TASKS<T> {
  CREATE: T;
  EDIT: T;
}

export interface RouteDefinitionDto<T> {
  APP: APP<T>;
  TASKS: TASKS<T>;
}
