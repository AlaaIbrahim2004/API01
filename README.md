# 🛒 E-Commerce RESTful API

> **🎓 Route IT Training Center - Backend (.NET Core) Diploma Project**
> *A highly scalable and robust backend API built with a focus on clean code principles, performance, and modern software architecture.*

## 📝 Project Overview
This repository contains the backend infrastructure for a fully-featured E-Commerce platform. The API is designed to handle complex product catalogs, secure user authentication, and high-speed cart operations, ensuring a seamless shopping experience. 

## 🏗️ System Architecture (Onion Architecture)
To ensure high maintainability, testability, and a strict separation of concerns, the solution is organized using **Onion Architecture** into the following decoupled layers:
* **Core Layer:** Contains the domain entities, business models, and core interfaces.
* **Infrastructure Layer:** Handles data access operations using **Entity Framework Core**, implementing the Repository Pattern and Unit of Work.
* **Service/Shared Layer:** Encapsulates business logic and utilizes **DTOs (Data Transfer Objects)** to securely map and format API responses.
* **Web (API) Layer:** The presentation layer containing controllers, routing, and dependency injection configurations.

## ✨ Key Features & Technical Highlights

* **🔒 Secure Authentication & Authorization:** Developed robust RESTful endpoints for user registration, login, and role management utilizing **ASP.NET Core Identity** secured with **JWT (JSON Web Tokens)**.
  
* **📦 Advanced Product Catalog Operations
