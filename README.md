Trip Booking API

A backend Trip Booking and Ticketing system built using ASP.NET Core Web API and Entity Framework Core as part of an advanced backend development training program.

🚀 Technologies Used
ASP.NET Core Web API
Entity Framework Core
SQL Server
LINQ
REST APIs
JWT Authentication
FluentValidation
Middleware
Repository Pattern
Unit of Work
Dependency Injection
HttpClient
Stored Procedures & Database Views
Async / Await
📌 Project Overview

This project simulates a real-world trip booking system where users can:

Browse and search trips
Book tickets and manage passengers
Create orders and manage bookings
Leave reviews and ratings

The system is built using clean backend architecture principles and scalable design patterns.

🏗️ Architecture

The project follows a layered architecture:

Controllers → Handle HTTP requests
Services → Business logic layer
Repositories → Data access layer
DbContext (EF Core) → Database communication
⚙️ Key Features & Concepts
Entity Framework Core
Code-First approach
Migrations and schema management
Relationships (One-to-One, One-to-Many, Many-to-Many)
Change tracking & AsNoTracking
Fluent API configuration
Eager loading with Include/ThenInclude
Data Operations
CRUD operations (Create, Read, Update, Delete)
LINQ querying (filtering, sorting, projection)
DTO-based data transfer optimization
Advanced Backend Concepts
Transactions using BeginTransactionAsync
Stored Procedures and Database Views
Raw SQL queries using FromSqlRaw / ExecuteSqlRaw
Architecture Patterns
Repository Pattern
Unit of Work Pattern
Dependency Injection
Clean separation of concerns
API Design
RESTful API design principles
Middleware pipeline (logging, request handling)
FluentValidation for input validation
Async/await for performance optimization
Testing & Debugging
Unit testing concepts for EF Core
SQL logging and query debugging
Validation pipeline testing
📊 Domain Model

Main entities include:

Users
Trips
Routes
Orders
Passengers
Tickets
Reviews
🎯 Purpose of the Project

This project was built to practice real-world backend engineering concepts including:

Scalable API architecture
Database design with EF Core
Clean code principles
Business logic separation
Production-style backend development
👩‍💻 Author

Mariam Tobasy
Computer Science Graduate | Backend & Full-Stack Developer

GitHub: https://github.com/mariamtobasy
LinkedIn: https://www.linkedin.com/in/mariam-tobasy-238ab7246/
⭐ Note

This project was developed as part of a structured .NET backend training program focusing on industry-level backend architecture and practices.
