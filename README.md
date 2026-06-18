# Multi-Tenant Employee API

A production-ready **.NET 10 Web API** demonstrating a robust multi-tenant architecture built using **Vertical Slice Architecture (VSA)**. The project replaces traditional rigid layering with cohesive, feature-based slices, implementing modern software engineering patterns including CQRS, Domain-Driven Design (DDD) principles, automated data auditing, and soft-deletes—fully containerized using Docker.

---

## 🏗️ Architecture & Technology Stack

The application is built around modern .NET paradigms, emphasizing loose coupling, high cohesion within features, and a highly maintainable code structure.

### Architectural Patterns
* **Vertical Slice Architecture (VSA):** Instead of dividing the application into traditional horizontal layers (Controllers, Services, Repositories), the system is organized around distinct business features or use cases. Each slice is self-contained and encapsulates its own domain logic, data mapping, and API endpoints.
* **CQRS (Command Query Responsibility Segregation):** Features are structured cleanly into discrete Commands (write operations) and Queries (read operations), processed via specialized in-memory `MediatR` handlers within their respective slices.
* **Multi-Tenancy:** Automated tenant resolution via standard HTTP headers handled gracefully through a dedicated `TenantService` and `IHttpContextAccessor`.
* **Domain-Driven Design (DDD) Elements:** Implementation of explicit Domain Models, Data Transfer Objects (DTOs), and immutability via Value Objects (e.g., `Money`).
* **Cross-Cutting Concerns:** * Automated data-auditing via an EF Core `AuditableEntityInterceptor`.
  * Global soft-delete query filters handled during EF model compilation.
  * Centralized pipeline request validation (`ValidationBehavior`).
  * Standardized global API error responses via a custom `ExceptionHandler` middleware.

### Key Technologies
* **Runtime & Framework:** `.NET 10` & `ASP.NET Core`
* **Routing & Minimal APIs:** `Carter` for elegant, modular, and maintainable endpoint mapping (`app.MapCarter()`). It allows endpoints to be declared directly inside their corresponding vertical slices, eliminating controller overhead.
* **Mediator Pattern:** `MediatR` to decouple HTTP endpoints from application logic using an in-memory Request/Response pipeline.
* **Validation:** `FluentValidation` for robust request validation, automatically piped through a centralized MediatR middleware behavior.
* **Data Persistence:** `Entity Framework Core` with `Npgsql` targeting a **PostgreSQL** instance.
* **Containerization:** `Docker` and `Docker Compose` for instant, cross-platform local development alignment.

---

---

## 🚀 How to Run the Project (One-Command Setup)

The entire application stack—including the .NET API application and a persistent PostgreSQL database instance—can be launched with a single command via Docker Compose.

### Prerequisites
Make sure you have [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running on your machine.

## 🚀 How to Run the Project (One-Command Setup)

The entire application stack—including the .NET API application and a persistent PostgreSQL database instance—can be launched with a single command via Docker Compose.

### Prerequisites
Make sure you have [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running on your machine.

### Step-by-Step Execution
1. **Clone the repository:**
   ```
   git clone [https://github.com/BahaaElMekkawy/Multi-Tenant-Employee-API-.git](https://github.com/BahaaElMekkawy/Multi-Tenant-Employee-API-.git)
   cd Multi-Tenant-Employee-API-
   ```
2.**Run the complete ecosystem**:
Open your terminal (PowerShell, CMD, or Bash) in the root folder and run:
```
    docker compose up --build
```
What happens automatically under the hood:
Spins up an isolated, healthy PostgreSQL container

Executes a multi-stage Docker compilation for the .NET 10 environment.

Automatically triggers EF Core database migrations (InitialCreate and Enable Soft Delete) against the fresh PostgreSQL instance on startup.

Opens the API to listen for active web requests on http://localhost:8080.

To stop the environment and shut down the services cleanly, press Ctrl + C or execute:

```
docker compose down
```

Running Automated Tests
The solution contains an isolated unit testing suite utilizing an optimized database emulator provider to quickly validate core features like business rule handlers, tenant data protection isolation, and pagination boundaries.

To run the full test suite directly from your host terminal using PowerShell or CMD, execute:

```
dotnet test MultiTenantEmployeeApi.slnx
```
