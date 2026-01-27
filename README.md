# MEGO Food 🍽️

MEGO Food is a **training and learning backend project** built with **ASP.NET Core** to practice modern backend development concepts such as **Clean Architecture, Domain-Driven Design (DDD), CQRS, and secure authentication**.

This project is created as a personal learning journey to apply best practices and advanced patterns in real-world scenarios.

---

## 🎯 Project Purpose
The main goal of MEGO Food is to:
- Practice Clean Architecture in a real backend project
- Apply Domain-Driven Design principles
- Implement CQRS using MediatR
- Build a secure and scalable authentication system
- Learn how to structure reusable and maintainable backend services

---

## 🧱 Architecture
The project follows **Clean Architecture**, organized into the following layers:

- **API Layer**
  - Controllers and endpoints
  - Swagger documentation
  - Global error handling

- **Application Layer**
  - Commands & Queries (CQRS)
  - MediatR for request handling
  - Pipeline Behaviors for cross-cutting concerns
  - FluentValidation and ErrorOr

- **Domain Layer**
  - Core entities
  - Domain methods (business rules)
  - Domain errors
  - Rich domain models independent of frameworks

- **Infrastructure Layer**
  - Entity Framework Core
  - ASP.NET Core Identity
  - Email service
  - Logging with Serilog
  - File handling and helper services

---

## 🔐 Authentication & Authorization
The authentication system is implemented using **CQRS and MediatR**, and includes:

- User registration and login
- Email confirmation with OTP
- Forgot and reset password flows
- JWT authentication with refresh tokens
- Role-based access control
- Secure token lifecycle management

---

## 🚀 Implemented Features
- User registration and login
- Email confirmation with OTP
- Forgot and reset password
- JWT authentication with refresh tokens
- Role-based access support
- Centralized validation and error handling
- Structured logging with Serilog
- Reusable infrastructure services

---

## 🔄 Pipeline Behaviors
MediatR **Pipeline Behaviors** are used to handle:
- Validation
- Cross-cutting concerns
- Cleaner separation between business logic and infrastructure

---

## 📦 Core Infrastructure Services
- Email Service
- Fuzzy Search Service
- Pagination Helper
- File Handling Utilities

---

## 🛠️ Tech Stack
- ASP.NET Core
- Clean Architecture
- MediatR
- CQRS
- FluentValidation
- ErrorOr
- Entity Framework Core
- ASP.NET Core Identity
- JWT Authentication & Refresh Tokens
- Serilog

---

## 🧠 Learning Outcomes
Through this project, I practice:
- Structuring large backend projects
- Applying domain-driven design concepts
- Implementing authentication securely
- Writing clean, maintainable, and testable code

---

## 👨‍💻 Author
**Ahmed**

Backend Developer (ASP.NET Core)  
Personal Training Project

---

## 📄 License
This project is created for learning and practice purposes.
