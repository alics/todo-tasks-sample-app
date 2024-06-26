# Todo Tasks Application

Welcome to the Todo Tasks Application! This project is a comprehensive example application that showcases both frontend and backend implementations using various technologies. 
It demonstrates how to apply Hexagonal and Clean Architecture principles, as well as Domain-Driven Design (DDD) concepts. 
The application is designed to be a useful reference for understanding these architectural patterns and their implementation in different programming languages and frameworks.

## Table of Contents
- [Project Overview](#project-overview)
- [Technologies Used](#technologies-used)
- [Architecture](#architecture)
- [Backend Implementations](#backend-implementations)
  - [C# .NET](#c-net)
  - [Node.js TypeScript](#nodejs-typescript)
  - [Golang](#golang)
- [Frontend Implementations](#frontend-implementations)
  - [React JS](#react-js)
  - [Angular](#angular)
- [Setup Instructions](#setup-instructions)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Project Overview

The Todo Tasks Application is a sample project designed to manage todo tasks. It includes basic CRUD (Create, Read, Update, Delete) operations for tasks, demonstrating a full-stack application with separate frontend and backend implementations.

## Technologies Used

### Backend
- **C# .NET**
- **Node.js TypeScript**
- **Golang**

### Frontend
- **React JS**
- **Angular**

## Architecture

The project follows Hexagonal Architecture (also known as Ports and Adapters) and Clean Architecture principles. It is designed to be highly maintainable, testable, and scalable. 
The use of Domain-Driven Design (DDD) ensures that the business logic is central to the application, making it easier to adapt to changing requirements.

### Key Architectural Concepts:
- **Domain Layer**: Contains the business logic and domain entities.
- **Application Layer**: Manages application-specific logic and use cases.
- **Infrastructure Layer**: Handles communication with external systems (e.g., databases, APIs).
- **Presentation Layer**: The frontend part of the application.

### Core Domain
Contains the business logic and domain entities related to Todo Task Management, independent of any external dependencies.

#### Entities and Value Objects: 
Domain entities such as TodoTask and value objects such as TaskHistory are modeled to represent domain concepts accurately.

#### Aggregates: 
TodoTask aggregate encapsulates consistency boundaries and enforces transactional integrity within the domain.

### Application Services: 
Serve as the entry point to the domain layer, encapsulating use cases and coordinating interactions between the domain layer and external interfaces.

### Adapters:
Adapts external interfaces such as controllers, and repositories to interact with the application core. 

### Tests
I have provided unit tests for the domain and application. I have also provided integration tests for API endpoints.

## Backend Implementations

### C# .NET

The C# .NET backend uses ASP.NET Core to build a robust API following Clean Architecture principles.

### Node.js TypeScript

The Node.js backend is built with TypeScript and follows similar architectural patterns. It uses:
- Nest framework for building efficient, scalable Node.js server-side applications
- TypeORM for data access

### Golang

The Golang backend showcases the simplicity and performance of Go with a structured approach:
- Fiber web framework
- GORM for ORM
- Dependency Injection using wire

## Frontend Implementations

### React JS

The React frontend is developed using modern React practices, including:
- Functional components and hooks
- React Router for navigation

### Angular

The Angular frontend leverages the powerful features of Angular framework:
- Components, Services, and Modules
- Angular Router for routing

## Setup Instructions

### Backend

#### C# .NET
1. Navigate to the `backend/dotnet` directory.
2. Restore dependencies: `dotnet restore`
3. Run the application: `dotnet run`

#### Node.js TypeScript
1. Navigate to the `backend/node.js-typescript` directory.
2. Install dependencies: `npm install`
3. Run the application: `npm start`

#### Golang
1. Navigate to the `backend/golang` directory.
2. Install dependencies: `go mod tidy`
3. Run the application: `go run main.go`

### Frontend

#### React JS
1. Navigate to the `frontend/react` directory.
2. Install dependencies: `npm install`
3. Run the application: `npm start`

#### Angular
1. Navigate to the `frontend/angular` directory.
2. Install dependencies: `npm install`
3. Run the application: `ng serve`

## Usage

Once the backend and frontend servers are running, you can interact with the To-Do Tasks Application through the web interface. The application allows you to create, view, update, and delete tasks.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. Ensure that your code follows the project's coding standards and include relevant tests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

Thank you for checking out the Todo Tasks Application. We hope it serves as a valuable resource for learning and implementing Hexagonal and Clean Architecture, as well as Domain-Driven Design concepts across different technologies.
