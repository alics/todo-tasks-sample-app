import React from "react";
import { Routes, Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";

import CreateTask from "./components/CreateTask";
import EditTask from "./components/EditTask";
import TasksList from "./components/TasksList";

const App: React.FC = () => {
  return (
    <div>
      <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <a href="/tasks" className="navbar-brand">
          Home
        </a>
        <div className="navbar-nav mr-auto">
          <li className="nav-item">
            <Link to={"/tasks"} className="nav-link">
              Tasks List
            </Link>
          </li>
          <li className="nav-item">
            <Link to={"/add"} className="nav-link">
              Create Task
            </Link>
          </li>
        </div>
      </nav>

      <div className="container mt-3">
        <Routes>
          <Route path="/" element={<TasksList/>} />
          <Route path="/tasks" element={<TasksList/>} />
          <Route path="/add" element={<CreateTask/>} />
          <Route path="/tasks/:id" element={<EditTask/>} />
        </Routes>
      </div>
    </div>
  );
}

export default App;