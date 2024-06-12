import http from "../utils/http-common";
import IResponseData from '../types/ResponseData';
import ITodoTaskData from "../types/TodoTaskData";

const getAll = () => {
  return http.get<IResponseData>("/tasks");
};

const get = (id: any) => {
  return http.get<ITodoTaskData>(`/tasks/${id}`);
};

const create = (data: ITodoTaskData) => {
  return http.post<ITodoTaskData>("/tasks", data);
};

const update = (id: any, data: ITodoTaskData) => {
  return http.put<any>(`/tasks/${id}`, data);
};

const remove = (id: any) => {
  return http.delete<any>(`/tasks/${id}`);
};

const findByTitle = (title: string) => {
  return http.get<IResponseData>(`/tasks?title=${title}`);
};

const TodoTasksService = {
  getAll,
  get,
  create,
  update,
  remove,
  findByTitle,
};

export default TodoTasksService;
