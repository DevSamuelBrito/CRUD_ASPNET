# 📝 CRUD ASP.NET Core API

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat&logo=csharp)
![Version](https://img.shields.io/badge/version-2.2.0-blue)

RESTful API for task management built with ASP.NET Core, following Clean Architecture principles and development best practices.

## ✨ Features

- ✅ Complete CRUD operations for Tasks (Create, Read, Update, Delete)
- ✅ Automatic validation with Data Annotations
- ✅ Global exception handling with standardized responses
- ✅ Structured logging with ILogger
- ✅ Layered architecture (Controller → Service → Repository)
- ✅ DTOs for separation of concerns
- ✅ Pagination support for large datasets
- ✅ AutoMapper for object mapping
- ✅ Automatic documentation with Swagger/OpenAPI
- ✅ CORS configured for frontend integration

## 🚀 Technologies

- **Framework:** ASP.NET Core 9.0
- **Language:** C# 12.0
- **ORM:** Entity Framework Core 9.0
- **Database:** PostgreSQL
- **Mapping:** AutoMapper
- **Documentation:** Swagger/OpenAPI
- **Validation:** Data Annotations

## 🏗️ Architecture

```
CRUD_ASPNET/
├── Controller/              # HTTP Endpoints (API)
├── Services/                # Business logic
│   ├── ITaskService.cs
│   └── TaskService.cs
├── Repositories/            # Data access
│   ├── ITaskRepository.cs
│   └── TaskRepository.cs
├── Models/                  # Database entities
│   ├── Tasks.cs
│   └── TaskStatus.cs
├── Application/
│   ├── DTO/                # Data Transfer Objects
│   │   ├── CreateTaskDTO.cs
│   │   ├── ReadTaskDto.cs
│   │   └── UpdateTaskDTO.cs
│   └── Mappings/           # AutoMapper profiles
│       └── TaskProfile.cs
├── Configuration/
│   └── Context/            # DbContext
│       └── AppDbContext.cs
├── Middleware/             # Exception handlers
│   └── GlobalExceptionHandlerMiddleware.cs
└── Migrations/             # EF Core Migrations
```

## 📋 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/) (version 12 or higher)
- Git
- Recommended IDE: Visual Studio Code or Visual Studio 2022

## 🔧 Installation and Setup

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

## 📖 API Documentation

Access the interactive Swagger documentation at `/api/docs` after starting the application.

### Available Endpoints

| Method | Endpoint         | Description          | Request Body  | Status Code   |
| ------ | ---------------- | -------------------- | ------------- | ------------- |
| GET    | `/api/task`      | List all tasks       | -             | 200           |
| GET    | `/api/task/{id}` | Get task by ID       | -             | 200, 404      |
| POST   | `/api/task`      | Create new task      | CreateTaskDTO | 201, 400      |
| PUT    | `/api/task/{id}` | Update existing task | UpdateTaskDTO | 200, 400, 404 |
| DELETE | `/api/task/{id}` | Delete task          | -             | 204, 404      |

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

### List all tasks

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

## 🌱 Branches

- `main` - Stable/production version
- `develop` - Active development
- `feat/*` - New features

## 📝 Roadmap / Future Improvements

- [x] Migration to PostgreSQL
- [x] Pagination in listings
- [x] Filtering and sorting
- [ ] Unit tests (xUnit)
- [ ] Docker and Docker Compose
- [x] CI/CD with GitHub Actions
- [ ] Rate limiting
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
