# Project Architecture Documentation Overview
This document provides an overview of the architecture of todo tasks simple applicaton, a .NET Web API application implementing hexagonal architecture, clean architecture, and Domain-Driven Design (DDD) principles. The project aims to provide a structured and modular approach for building and maintaining a Todo Task Management system.

### Tests
I have provided unit tests for domain and application. I have also provided integration tests for API endpoints.

## Running application Notes:
There is a sql script **(migration-script.sql)** in **TodoApplication.Persistence** project. Please create a database with name **'Todo'** in Visual Stuio local sql Database ((localdb)\MSSQLLocalDB) and execute this script on it to create a database for the application
