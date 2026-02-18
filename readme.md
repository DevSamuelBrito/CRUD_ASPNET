# 📝 CRUD ASP.NET Core API

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat&logo=csharp)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791?style=flat&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Enabled-2496ED?style=flat&logo=docker&logoColor=white)
![Version](https://img.shields.io/badge/version-2.3.0-blue)

RESTful API for task management built with ASP.NET Core, following Clean Architecture principles and development best practices.

## ✨ Features

- ✅ Complete CRUD operations for Tasks (Create, Read, Update, Delete)
- ✅ Automatic validation with Data Annotations
- ✅ Global exception handling with standardized responses
- ✅ Structured logging with ILogger
- ✅ Clean Architecture with separated layers (API, Application, Domain, Infrastructure)
- ✅ DTOs for separation of concerns
- ✅ Pagination support with filtering (by title and status)
- ✅ AutoMapper for object mapping
- ✅ Automatic documentation with Swagger/OpenAPI
- ✅ CORS configured for frontend integration
- ✅ Rate limiting to protect against abuse
- ✅ Unit tests (xUnit) for Application Services
- ✅ Docker containerization with Docker Compose
- ✅ Automatic database migrations on startup

## 🚀 Technologies

- **Framework:** ASP.NET Core 9.0
- **Language:** C# 12.0
- **ORM:** Entity Framework Core 9.0
- **Database:** PostgreSQL 16
- **Containerization:** Docker & Docker Compose
- **Mapping:** AutoMapper
- **Documentation:** Swagger/OpenAPI
- **Validation:** Data Annotations
- **Rate Limiting:** ASP.NET Core Built-in Rate Limiter
- **Testing:** xUnit

## 🏗️ Architecture

The project follows **Clean Architecture** principles and is organized into separate layers using **Class Libraries**:

```
CRUD_ASPNET/
├── CRUD_ASPNET.API/                    # 🌐 Presentation Layer
│   ├── Controller/                      # HTTP Endpoints (API Controllers)
│   │   └── TaskController.cs
│   ├── Extensions/                      # Extension methods
│   │   ├── ServiceCollectionExtensions.cs
│   │   └── WebApplicationExtensions.cs
│   ├── Middleware/                      # Exception handlers & middlewares
│   │   └── GlobalExceptionHandlerMiddleware.cs
│   ├── Properties/                      # Project properties
│   │   └── launchSettings.json
│   ├── appsettings.json                # App configuration
│   ├── appsettings.Development.json    # Development configuration
│   ├── CRUD_ASPNET.API.csproj          # Project file
│   ├── Dockerfile                      # Docker image configuration
│   └── Program.cs                       # Application configuration
│
├── CRUD_ASPNET.Application/            # 📋 Application Layer
│   ├── DTO/                            # Data Transfer Objects
│   │   ├── CreateTaskDto.cs
│   │   ├── ReadTaskDto.cs
│   │   ├── UpdateTaskDto.cs
│   │   └── GetParametersDTO.cs         # DTO for pagination
│   ├── Services/                        # Business logic
│   │   ├── Interfaces/
│   │   │   └── ITaskService.cs
│   │   └── TaskService.cs
│   ├── Mappings/                       # AutoMapper profiles
│   │   └── TaskProfile.cs
│   └── CRUD_ASPNET.Application.csproj  # Project file
│
├── CRUD_ASPNET.Domain/                 # 🎯 Domain Layer
│   ├── Entities/                        # Domain entities
│   │   ├── Tasks.cs
│   │   └── TaskStatus.cs
│   └── CRUD_ASPNET.Domain.csproj       # Project file
│
├── CRUD_ASPNET.Infra/                  # 🗄️ Infrastructure Layer
│   ├── Infra/
│   │   ├── Configuration/               # Entity configurations
│   │   │   └── Context/
│   │   │       └── AppDbContext.cs     # EF Core DbContext
│   │   ├── Repositories/                # Data access patterns
│   │   │   ├── Interfaces/
│   │   │   │   └── ITaskRepository.cs
│   │   │   └── TaskRepository.cs
│   │   └── Pagination/                  # Pagination utilities
│   │       └── PagedList.cs
│   ├── Migrations/                      # EF Core migrations
│   │   ├── 20260102232311_InitialCreate.cs
│   │   ├── 20260102232311_InitialCreate.Designer.cs
│   │   └── AppDbContextModelSnapshot.cs
│   └── CRUD_ASPNET.Infra.csproj        # Project file
│
├── CRUD_ASPNET.Tests/                  # 🧪 Test Layer
│   ├── Services/                        # Service unit tests
│   │   └── TaskServiceTests.cs
│   └── CRUD_ASPNET.Tests.csproj        # Project file
│
├── CRUD_ASPNET.sln                     # Solution file
├── docker-compose.yml                  # 🐳 Docker orchestration
└── readme.md                            # This file
```

### Separation of Concerns

- **API Layer:** Handles HTTP requests and returns responses
- **Application Layer:** Contains business logic and orchestration
- **Domain Layer:** Defines entities and domain business rules
- **Infrastructure Layer:** Implements persistence, data access, and external features
- **Test Layer:** Unit and integration tests

## 📋 Prerequisites

### Option 1: Running with Docker (Recommended)

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- Git

### Option 2: Running Locally

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/) (version 12 or higher)
- Git
- Recommended IDE: Visual Studio Code or Visual Studio 2022

## 🔧 Installation and Setup

### 🐳 Option 1: Running with Docker (Recommended)

Docker Compose provides the easiest way to run the application with all dependencies configured automatically.

#### What's included:

- **API Container:** ASP.NET Core 9.0 application
- **Database Container:** PostgreSQL 16
- **Automatic migrations:** Database schema is created on startup
- **Persistent storage:** Database data is preserved between restarts
- **Network isolation:** Containers communicate on a private network

#### Quick Start:

```bash
# 1. Clone the repository
git clone https://github.com/DevSamuelBrito/CRUD_ASPNET.git
cd CRUD_ASPNET

# 2. Start the application
docker compose up --build

# Or run in detached mode (background)
docker compose up -d --build
```

The API will be available at:

- **API:** http://localhost:8080
- **Swagger Documentation:** http://localhost:8080/api/docs
- **PostgreSQL:** localhost:5432

#### Docker Commands:

```bash
# Stop the application
docker compose down

# Stop and remove all data (including database)
docker compose down -v

# View logs
docker compose logs -f

# View logs for a specific service
docker compose logs -f api
docker compose logs -f db

# Restart services
docker compose restart

# Check running containers
docker compose ps
```

#### Docker Configuration Files:

**docker-compose.yml:**

- Orchestrates both API and PostgreSQL containers
- Configures networking, ports, and environment variables
- Sets up health checks for the database
- Defines persistent volume for database data

**CRUD_ASPNET.API/dockerfile:**

- Multi-stage build for optimized image size
- Stage 1: Build the application using .NET SDK 9.0
- Stage 2: Runtime with ASP.NET Core 9.0 (smaller image)
- Exposes port 8080

#### Environment Variables (Docker):

The following environment variables are configured in `docker-compose.yml`:

- `ASPNETCORE_ENVIRONMENT=Development` - Enables Swagger and developer features
- `ConnectionStrings__DefaultConnection` - PostgreSQL connection string (automatically configured)

#### Troubleshooting Docker:

**Port already in use:**

```bash
# Check what's using port 8080
lsof -i :8080

# Change the port in docker-compose.yml
ports:
  - "9090:8080"  # Use port 9090 instead
```

**Database connection issues:**

```bash
# Verify database is healthy
docker compose ps

# Check database logs
docker compose logs db
```

**Clean restart:**

```bash
# Remove all containers and volumes
docker compose down -v

# Rebuild and start
docker compose up --build
```

---

### 💻 Option 2: Running Locally (Without Docker)

If you prefer to run the application without Docker:

### 1. Clone the repository

```bash
git clone https://github.com/DevSamuelBrito/CRUD_ASPNET.git

cd CRUD_ASPNET
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Configure PostgreSQL database

#### 3.1. Create PostgreSQL database

First, create a database in PostgreSQL:

```sql
CREATE DATABASE crud_aspnet;
```

#### 3.2. Configure connection string

You have two options for configuring the PostgreSQL connection string:

**Option A: Using User Secrets (Recommended for development)**

Initialize and configure user secrets:

```bash
# Initialize user secrets
dotnet user-secrets init

# Set connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=crud_aspnet;Username=your_user;Password=your_password"
```

**Option B: Environment Variable**

Set the connection string as an environment variable:

```bash
# macOS/Linux
export ConnectionStrings__DefaultConnection="Host=localhost;Database=crud_aspnet;Username=your_user;Password=your_password"

# Windows (PowerShell)
$env:ConnectionStrings__DefaultConnection="Host=localhost;Database=crud_aspnet;Username=your_user;Password=your_password"
```

**Option C: appsettings.Development.json (Not recommended - for testing only)**

⚠️ **Warning:** Never commit database credentials to version control!

Edit `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=crud_aspnet;Username=your_user;Password=your_password"
  }
}
```

**Connection String Parameters:**

- `Host`: PostgreSQL server address (usually `localhost` for local development)
- `Database`: Database name (e.g., `crud_aspnet`)
- `Username`: Your PostgreSQL username
- `Password`: Your PostgreSQL password
- `Port`: PostgreSQL port (default: 5432, optional if using default)

### 4. Run migrations

```bash
dotnet ef database update
```

### 5. Run the application

```bash
dotnet run
```

The API will be available at:

- **HTTP:** http://localhost:5272
- **HTTPS:** https://localhost:7217
- **Swagger:** http://localhost:5272/api/docs

### 6. Run the Tests

```bash
dotnet test
```

## 📖 API Documentation

Access the interactive Swagger documentation at `/api/docs` after starting the application.

### Available Endpoints

| Method | Endpoint              | Description                | Request Body  | Query Params                        | Status Code   |
| ------ | --------------------- | -------------------------- | ------------- | ----------------------------------- | ------------- |
| GET    | `/api/task`           | List all tasks             | -             | -                                   | 200           |
| GET    | `/api/task/paginated` | List tasks with pagination | -             | PageNumber, PageSize, title, status | 200, 400      |
| GET    | `/api/task/{id}`      | Get task by ID             | -             | -                                   | 200, 404      |
| POST   | `/api/task`           | Create new task ⚡         | CreateTaskDTO | -                                   | 201, 400, 429 |
| PUT    | `/api/task/{id}`      | Update existing task       | UpdateTaskDTO | -                                   | 200, 400, 404 |
| DELETE | `/api/task/{id}`      | Delete task                | -             | -                                   | 204, 404      |

### Pagination Parameters

| Parameter  | Type       | Default | Description                      | Required |
| ---------- | ---------- | ------- | -------------------------------- | -------- |
| PageNumber | int        | 1       | Page number (starts at 1)        | No       |
| PageSize   | int        | 20      | Items per page (max 100)         | No       |
| title      | string     | null    | Filter by title (partial search) | No       |
| status     | TaskStatus | null    | Filter by status (1, 2, or 3)    | No       |

### Task Status Enum

| Value | Description |
| ----- | ----------- |
| `1`   | ToDo        |
| `2`   | Doing       |
| `3`   | Done        |

## 💡 Usage Examples

### Create a new task

**Request:**

```http
POST /api/task
Content-Type: application/json

{
  "title": "Implement JWT authentication",
  "description": "Add authentication system with JWT tokens",
  "status": 1
}
```

**Response (201 Created):**

```json
{
  "id": 1,
  "title": "Implement JWT authentication",
  "description": "Add authentication system with JWT tokens",
  "status": 1
}
```

### List all tasks (without pagination)

**Request:**

```http
GET /api/task
```

**Response (200 OK):**

```json
[
  {
    "id": 1,
    "title": "Implement JWT authentication",
    "description": "Add authentication system with JWT tokens",
    "status": 1
  },
  {
    "id": 2,
    "title": "Configure CI/CD",
    "description": "Setup GitHub Actions pipeline",
    "status": 2
  }
]
```

### List tasks with pagination and filters

**Request:**

```http
GET /api/task/paginated?PageNumber=1&PageSize=10&title=auth&status=1
```

**Response (200 OK):**

```json
{
  "data": [
    {
      "id": 1,
      "title": "Implement JWT authentication",
      "description": "Add authentication system with JWT tokens",
      "status": 1
    }
  ],
  "currentPage": 1,
  "totalPages": 1,
  "pageSize": 10,
  "totalCount": 1,
  "hasPrevious": false,
  "hasNext": false
}
```

**Response (400 Bad Request) - PageSize too large:**

```json
{
  "error": "PageSize cannot be greater than 100."
}
```

### Update a task

**Request:**

```http
PUT /api/task/1
Content-Type: application/json

{
  "title": "Implement JWT authentication",
  "description": "Add authentication system with JWT tokens - COMPLETED",
  "status": 3
}
```

**Response (200 OK):**

```json
{
  "id": 1,
  "title": "Implement JWT authentication",
  "description": "Add authentication system with JWT tokens - COMPLETED",
  "status": 3
}
```

### Delete a task

**Request:**

```http
DELETE /api/task/1
```

**Response (204 No Content):**

```
(empty)
```

## ⚠️ Error Handling

The API returns standardized error responses:

### Validation (400 Bad Request)

```json
{
  "status": 400,
  "title": "One or more validation errors occurred.",
  "errors": {
    "Title": ["Title must be between 3 and 200 characters"],
    "Status": ["Status must be ToDo (1), Doing (2), or Done (3)"]
  }
}
```

### Not Found (404 Not Found)

```json
{
  "statusCode": 404,
  "message": "Task with id 999 not found."
}
```

### Internal Error (500 Internal Server Error)

```json
{
  "statusCode": 500,
  "message": "An unexpected error occurred. Please try again later."
}
```

## 🛡️ Rate Limiting

The API implements rate limiting to protect against abuse and ensure fair usage:

### Global Rate Limit

- **Limit:** 100 requests per minute per IP address
- **Applies to:** All endpoints
- **Window:** Fixed window of 1 minute

### Strict Rate Limit ⚡

- **Limit:** 10 requests per minute
- **Applies to:** POST `/api/task` (Create new task)
- **Window:** Fixed window of 1 minute

### Rate Limit Response (429 Too Many Requests)

When you exceed the rate limit, the API returns:

```json
{
  "statusCode": 429,
  "message": "Too many requests. Please try again later."
}
```

**Response Headers:**

- `Retry-After`: Time in seconds until you can make requests again

**Best Practices:**

- Implement exponential backoff in your client applications
- Cache responses when possible to reduce API calls
- Monitor rate limit headers in production

## 🌱 Branches

- `main` - Stable/production version
- `develop` - Active development
- `feat/*` - New features

## 📝 Roadmap / Future Improvements

- [x] Migration to PostgreSQL
- [x] Pagination in listings
- [x] Filtering and sorting
- [x] Unit tests (xUnit)
- [x] Clean Architecture with Class Libraries
- [x] Rate limiting (Global + Endpoint-specific)
- [x] Docker and Docker Compose
- [x] CI/CD with GitHub Actions
- [x] API versioning

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the project
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 👨‍💻 Author

**DevSamuelBrito**

- GitHub: [DevSamuelBrito](https://github.com/DevSamuelBrito)
- LinkedIn: [Samuel Fava de Brito](https://www.linkedin.com/in/samuel-fava-de-brito/)
- Email: samuelbrito.dev@gmail.com

---
