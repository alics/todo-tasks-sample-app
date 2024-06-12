import { BaseEntity, Column, Entity, JoinColumn, ManyToOne, PrimaryGeneratedColumn } from 'typeorm';
import { TaskStatus } from 'src/common/task-status';
import { TaskEntity } from './task.entity';

@Entity('Task_Histories')
export class TaskHistoryEntity extends BaseEntity {
    /**
     * Initializes a new instance of the TaskHistoryEntity class.
     * @param status - The status of the task history.
     * @param dateTime - The date and time of the task history.
     * @param task - The task associated with the history.
     */
    constructor(status: TaskStatus, dateTime: Date, task: TaskEntity) {
        super();
        this.status = status;
        this.dateTime = dateTime;
        this.task = task;
    }

    /**
     * The ID of the task history.
     */
    @PrimaryGeneratedColumn()
    id: number;

    /**
     * The status of the task history.
     */
    @Column()
    status: TaskStatus;

    /**
     * The date and time of the task history.
     */
    @Column()
    dateTime: Date;

    /**
     * The task associated with the history.
     */
    @ManyToOne(type => TaskEntity, task => task.histories, { onDelete: 'CASCADE', nullable: false })
    @JoinColumn({ name: 'taskId' })
    task: TaskEntity;
}
