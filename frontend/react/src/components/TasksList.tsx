import React, { useState, useEffect, ChangeEvent } from "react";
import TodoTasksService from "../services/TodoTasksService";
import ITodoTaskData from '../types/TodoTaskData';
import { format } from 'date-fns';
import Table from 'react-bootstrap/Table';
import { TaskStatus } from '../types/TaskStatus';
import ConfirmationPopup from '../utils/confirmationPopup';

/**
 * Component for displaying a list of tasks with search and delete functionalities.
 */
const TasksList: React.FC = () => {
  // State variables
  const [tasks, setTasks] = useState<ITodoTaskData[]>([]);
  const [searchTitle, setSearchTitle] = useState<string>("");
  const [showDeleteConfirmation, setShowDeleteConfirmation] = useState<boolean>(false);
  const [deletionTask, setDeletionTask] = useState<ITodoTaskData | undefined>();

  // Fetch tasks on component mount
  useEffect(() => {
    retrieveTasks();
  }, []);

  /**
   * Fetches all tasks from the backend and updates the tasks state.
   */
  const retrieveTasks = async () => {
    try {
      const response = await TodoTasksService.getAll();
      setTasks(response.data.items);
    } catch (error) {
      console.error(error);
    }
  };

  /**
   * Updates the searchTitle state based on user input.
   * @param e The change event object.
   */
  const onChangeSearchTitle = (e: ChangeEvent<HTMLInputElement>) => {
    setSearchTitle(e.target.value);
  };

  /**
   * Searches tasks by title and updates the tasks state.
   */
  const findByTitle = async () => {
    try {
      const response = await TodoTasksService.findByTitle(searchTitle);
      setTasks(response.data.items);
    } catch (error) {
      console.error(error);
    }
  };

  /**
   * Deletes a task from the backend and updates the tasks state.
   * @param task The task to be deleted.
   */
  const deleteTask = async (task: ITodoTaskData) => {
    try {
      await TodoTasksService.remove(task.id);
      retrieveTasks();
    } catch (error) {
      console.error(error);
    }
  };

  /**
   * Handles the click event for deleting a task.
   * @param task The task to be deleted.
   */
  const handleDelete = (task: ITodoTaskData) => {
    setDeletionTask(task);
    setShowDeleteConfirmation(true);
  };

  /**
   * Confirms the deletion of the task and hides the confirmation popup.
   */
  const handleConfirmDelete = () => {
    if (deletionTask) {
      deleteTask(deletionTask);
      setShowDeleteConfirmation(false);
    }
  };

  /**
   * Closes the delete confirmation popup.
   */
  const handleCloseConfirmation = () => {
    setShowDeleteConfirmation(false);
  };

  /**
   * Redirects to the edit page for the specified task.
   * @param taskId The ID of the task to be edited.
   */
  const handleEdit = (taskId: number) => {
    window.location.href = `/tasks/${taskId}`;
  };

  return (
    <div className="list row">
      {/* Search input */}
      <div className="col-md-8">
        <div className="input-group mb-3">
          <input
            type="text"
            className="form-control mr-2"
            placeholder="Search by title"
            value={searchTitle}
            onChange={onChangeSearchTitle}
          />
          <div className="input-group-append">
            <button
              className="btn btn-primary ml-2"
              type="button"
              onClick={findByTitle}
            >
              Search
            </button>
          </div>
        </div>
      </div>
      {/* Task list */}
      <div className="col-md-6">
        <Table className="table table-hover">
          <thead>
            <tr>
              <th>#</th>
              <th>Title</th>
              <th>Status</th>
              <th>Deadline</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {tasks.map((task, index) => (
              <tr key={task.id} className={new Date(task.deadline!) < new Date() ? 'table-danger' : ''}>
                <td>{index + 1}</td>
                <td>{task.title}</td>
                <td>{TaskStatus[task.status!]}</td>
                <td>{format(new Date(task.deadline!), 'yyyy-MM-dd')}</td>
                <td>
                  {/* Edit and delete buttons */}
                  <button
                    className="btn btn-success mr-2"
                    onClick={() => handleEdit(task.id)}
                  >
                    Edit
                  </button>
                  <button
                    className="btn btn-danger"
                    onClick={() => handleDelete(task)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        {/* Confirmation popup for task deletion */}
        <ConfirmationPopup
          show={showDeleteConfirmation}
          handleClose={handleCloseConfirmation}
          handleConfirm={handleConfirmDelete}
        />
      </div>
    </div>
  );
};

export default TasksList;
