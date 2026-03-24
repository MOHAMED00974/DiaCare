# 🩺 DiaCare - Smart Support System for Diabetes Patients

**DiaCare** is a comprehensive management and support system designed to help diabetes patients track their health, predict risks using AI, and access educational resources.

##  Architecture & Design Patterns
This project follows **Clean Architecture** principles to ensure scalability, maintainability, and testability.

### 🛠️ Patterns Implemented:
* **Repository Pattern (Generic):** To abstract data access logic and promote the SOLID principles.
* **Unit of Work:** To coordinate multiple repositories and ensure atomic database transactions.
* **Dependency Injection:** To decouple layers and manage service lifetimes.
* **Layered Architecture:** Clear separation between Domain, Application, Infrastructure, and WebAPI.

##  Tech Stack
* **.NET 8 Web API**
* **Entity Framework Core**
* **SQL Server (LocalDB)**
* **ASP.NET Core Identity** (Authentication & Authorization)
* **JWT** (Planned for secure communication)

## 📁 Project Structure
* **DiaCare.Domain:** Entities, Interfaces, and Domain Logic.
* **DiaCare.Application:** Services, DTOs, and Business Logic.
* **DiaCare.Infrastructure:** Data Access, Persistence, and External Services.
* **DiaCare.WebAPI:** Controllers and API Configurations.
