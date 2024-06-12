import React, { useState, ChangeEvent, FormEvent } from "react";
import TodoTasksService from "../services/TodoTasksService";
import ITodoTaskData from '../types/TodoTaskData';
import { useNavigate } from "react-router-dom";

/**
 * Component for creating new tasks.
 */
const CreateTask: React.FC = () => {
  // Initial state for a new task
  const initialTaskState: ITodoTaskData = {
    id: null,
    title: "",
    status: 0,
  };

  // State variables
  const [task, setTask] = useState<ITodoTaskData>(initialTaskState);
  const [createTaskError, setCreateTaskError] = useState<string>('');
  const [createTaskErrorFlag, setCreateTaskErrorFlag] = useState<boolean>(false);
  const navigate = useNavigate();

  /**
   * Handles changes in input fields and updates the task state accordingly.
   * @param event The change event triggered by input fields.
   */
  const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setTask(prevTask => ({ ...prevTask, [name]: value }));
  };

  /**
   * Handles form submission to create a new task.
   * @param e The form submission event.
   */
  const saveTask = async (e: FormEvent) => {
    e.preventDefault();
    try {
      // Send request to create task
      const response = await TodoTasksService.create(task);
      console.log(response.data);
      // Navigate to the tasks page after successful creation
      navigate("/tasks");
    } catch (error: any) {
      // Handle error if task creation fails
      setCreateTaskErrorFlag(true);
      setCreateTaskError(error.response?.data?.errorMessage || error.response?.data?.title || 'An error occurred');
    }
  };

  return (
    <form className="submit-form needs-validation" onSubmit={saveTask} >
      <div className="card p-4 mb-3">
        <h5 className="card-title">Create Task</h5>
        {/* Input field for task title */}
        <div className="form-group mb-3">
          <label htmlFor="title">Title</label>
          <input
            type="text"
            className="form-control"
            id="title"
            minLength={10}
            required
            value={task.title}
            onChange={handleInputChange}
            name="title"
          />
        </div>

        {/* Input field for task deadline */}
        <div className="form-group mb-3">
          <label htmlFor="deadline">Deadline</label>
          <input
            type="date"
            className="form-control"
            id="deadline"
            required
            onChange={handleInputChange}
            name="deadline"
          />
        </div>

        {/* Submit button */}
        <button type="submit" className="btn btn-success">
          Submit
        </button>
      </div>

      {/* Display error message if task creation fails */}
      {createTaskErrorFlag && (
        <div className="alert alert-danger" role="alert">
          {createTaskError}
        </div>
      )}
    </form>
  );
};

export default CreateTask;
