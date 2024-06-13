# Project Architecture Documentation Overview
This document provides an overview of the architecture of todo tasks simple applicaton, a .NET Web API application implementing hexagonal architecture, clean architecture, and Domain-Driven Design (DDD) principles. The project aims to provide a structured and modular approach for building and maintaining a Todo Task Management system.

# Hexagonal and Clean Architecture
Hexagonal architecture, also known as Ports and Adapters architecture, is utilized in todo tasks application to achieve loose coupling and separation of concerns. The key components of the hexagonal architecture in this project include:

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
I have provided unit tests for domain and application. I have also provided integration tests for API endpoints.

## Running application Notes:
There is a sql script **(migration-script.sql)** in **TodoApplication.Persistence** project. Please create a database with name **'Todo'** in Visual Stuio local sql Database ((localdb)\MSSQLLocalDB) and execute this script on it to create a database for the application
