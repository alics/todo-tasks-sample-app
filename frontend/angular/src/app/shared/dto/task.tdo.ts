import { TaskStatus } from "./task-status";

export interface TaskDto {
    id?: any | null,
    title: string,
    deadline?: Date,
    status?: TaskStatus,
  }
