import { TaskStatus } from './task-status';

export interface Item {
  id: string;
  title: string;
  creationDate: string;
  deadlineDate: string;
  status: TaskStatus;
}
