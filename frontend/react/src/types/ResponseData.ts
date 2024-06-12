import { TaskStatus } from './TaskStatus'

  export default interface IResponseData {
    totalItems: number
    items: Item[]
  }
  
  export  interface Result {
    totalItems: number
    items: Item[]
  }
  
  export interface Item {
    id: string
    title: string
    creationDate: string
    deadlineDate: string
    status: TaskStatus
  }

  export interface Error {
    code: number
    message: string
  }
  
  
 
  