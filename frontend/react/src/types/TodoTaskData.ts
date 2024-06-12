import { TaskStatus } from './TaskStatus';

export default interface ITodoTaskData {
  id?: any | null,
  title: string,
  deadline?: Date,
  status?: TaskStatus,
}