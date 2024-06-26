import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import ResponseData from '../dto/response-data.dto';
import { TaskDto } from '../dto/task.tdo';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = 'http://localhost:5094/api';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient) {}

  getAll(): Observable<ResponseData> {
    return this.http.get<ResponseData>(`${this.baseUrl}/tasks`);
  }

  get(id: string): Observable<TaskDto> {
    return this.http.get<TaskDto>(`${this.baseUrl}/tasks/${id}`);
  }

  create(data: TaskDto): Observable<TaskDto> {
    return this.http.post<TaskDto>(`${this.baseUrl}/tasks`, data);
  }

  update(id: string, data: TaskDto): Observable<unknown> {
    return this.http.put<unknown>(`${this.baseUrl}/tasks/${id}`, data);
  }

  remove(id: string): Observable<unknown> {
    return this.http.delete<unknown>(`${this.baseUrl}/tasks/${id}`);
  }

  findByTitle(title: string): Observable<ResponseData> {
    return this.http.get<ResponseData>(`${this.baseUrl}/tasks?title=${title}`);
  }
}
