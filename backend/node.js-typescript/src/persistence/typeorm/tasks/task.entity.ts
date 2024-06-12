import { BaseEntity, Column, Entity, JoinColumn, OneToMany, PrimaryColumn } from 'typeorm';
import { IsString, MaxLength, MinLength } from 'class-validator';
import { TaskStatus } from 'src/common/task-status';
import { TaskHistoryEntity } from './task-history.entity';

@Entity('Tasks')
export class TaskEntity extends BaseEntity {
  /**
   * Initializes a new instance of the TaskEntity class.
   * @param id - The ID of the task.
   * @param title - The title of the task.
   * @param status - The status of the task.
   * @param deadline - The deadline of the task.
   * @param creationTime - The creation time of the task.
   */
  constructor(id: string, title: string, status: TaskStatus, deadline: Date, creationTime: Date) {
    super();
    this.id = id;
    this.title = title;
    this.status = status;
    this.deadline = deadline;
    this.creationDate = creationTime;
  }

  /**
   * The ID of the task.
   */
  @PrimaryColumn({ type: 'uuid', nullable: false })
  id: string;

  /**
   * The title of the task.
   */
  @Column({ length: 500 })
  @MinLength(10)
  @MaxLength(500)
  @IsString()
  title: string;

  /**
   * The status of the task.
   */
  @Column()
  status: TaskStatus;

  /**
   * The creation time of the task.
   */
  @Column()
  creationDate: Date;

  /**
   * The deadline of the task.
   */
  @Column()
  deadline: Date;

  /**
   * The task histories associated with the task.
   */
  @OneToMany(type => TaskHistoryEntity, history => history.task, { eager: true, cascade: true, onDelete: 'CASCADE' })
  @JoinColumn()
  histories: TaskHistoryEntity[];
}