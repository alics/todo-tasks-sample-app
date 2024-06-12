import React, { useState, useEffect, ChangeEvent } from "react";
import { useParams } from 'react-router-dom';
import { format } from 'date-fns';
import TodoTasksService from "../services/TodoTasksService";
import ITodoTaskData from "../types/TodoTaskData";
import { TaskStatus } from '../types/TaskStatus';
import { getEnumKeys } from '../utils/common';

/**
 * Component for editing a task.
 */
const EditTask: React.FC = () => {
  // Get task ID from URL params
  const { id } = useParams<{ id: string }>();

  // State variables
  const [currentTask, setCurrentTask] = useState<ITodoTaskData | null>(null);
  const [message, setMessage] = useState<string>("");
  const [currentTaskStatus, setCurrentTaskStatus] = useState<TaskStatus>(TaskStatus.Created);

  // Fetch task based on ID
  useEffect(() => {
    const getTask = async () => {
      try {
        const response = await TodoTasksService.get(id);
        if (response.status === 200) {
          const task = response.data as ITodoTaskData;
          setCurrentTask(response.data);
          setCurrentTaskStatus(task.status!);
        } else {
          setMessage("Invalid route!")
        }
      } catch (error: any) {
        console.error(error);
      }
    };

    if (id) getTask();
  }, [id]);

  /**
   * Handles input changes in the form fields.
   * @param event The change event object.
   */
  const handleInputChange = (event: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = event.target;
    setCurrentTask(prevTask => ({
      ...prevTask!,
      [name]: name === 'deadline' ? new Date(value) : value
    }));
  };

  /**
   * Updates the task status and sends the updated task data to the backend.
   */
  const updateTask = async () => {
    try {
      if (!currentTask) return;
      currentTask.status = currentTaskStatus;
      await TodoTasksService.update(currentTask.id, currentTask);
      setCurrentTask({ ...currentTask }); // Refresh current task state
      setMessage("The status was updated successfully!");
    } catch (error: any) {
      console.error(error.response?.data);
      setMessage(error.response?.data?.errorMessage || error.response?.data?.title || "An error occurred");
    }
  };

  return (
    <div>
      {currentTask ? (
        <div className="card p-4 mb-3">
          <h5 className="card-title">Update Task</h5>
          <form>
            {/* Input fields for editing task */}
            <div className="form-group mb-3">
              <label htmlFor="title">Title</label>
              <input
                type="text"
                className="form-control"
                id="title"
                name="title"
                value={currentTask.title}
                onChange={handleInputChange}
              />
            </div>
            <div className="form-group mb-3">
              <label htmlFor="deadline">Deadline</label>
              <input
                type="date"
                className="form-control"
                id="deadline"
                name="deadline"
                value={format(currentTask.deadline!, 'yyyy-MM-dd')}
                onChange={handleInputChange}
              />
            </div>
            <div className="form-group mb-3">
              <label htmlFor="status">Status</label>
              {/* Dropdown for selecting task status */}
              <select
                className="form-select"
                value={currentTaskStatus}
                onChange={(e) => setCurrentTaskStatus(parseInt(e.target.value) as TaskStatus)}
              >
                {/* Generate dropdown options from TaskStatus enum */}
                {getEnumKeys(TaskStatus).map((key, index) => (
                  <option key={index} value={TaskStatus[key]}>
                    {key}
                  </option>
                ))}
              </select>
            </div>
          </form>
          {/* Button to update task */}
          <button
            type="button"
            className="btn btn-success mt-2 col-md-12"
            onClick={updateTask}
          >
            Update Task
          </button>
        </div>
      ) : (
        <div >
        </div>
      )}
      <div>
        {/* Display message */}
        {message && (
          <div className="alert alert-light mt-3" role="alert">
            {message}
          </div>
        )}
      </div>
    </div>
  );
};

export default EditTask;
