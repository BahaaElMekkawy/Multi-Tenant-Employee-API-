# Multi-Tenant Employee API

A production-ready, highly secure .NET 10 Web API utilizing modern architectural patterns to implement a robust **Multi-Tenant** system with complete data isolation per tenant. 

##  Architecture & Features

* **Multi-Tenancy Isolation:** Implements isolated tenant scopes resolved per request via a custom HTTP Header (`X-Tenant-Id`).
* **Global Query Filters:** Built safely with EF Core query filters to automatically restrict data access boundaries, preventing cross-tenant data leaks.
* **PostgreSQL Native Handling:** Supports schema-less dynamic data using the native PostgreSQL `jsonb` column engine mapped via .NET `JsonDocument`.
* **Soft Deletes:** Intercepts deletions to preserve audit records while hiding them from active application workflows.
* **Containerized Environment:** Fully orchestrated multi-stage container deployment linking the API service with a live database engine.

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
