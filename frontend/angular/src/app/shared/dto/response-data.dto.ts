import { TaskDto } from './task.tdo';

export default interface ResponseData {
  totalItems: number;
  items: TaskDto[];
}
