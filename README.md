# 📝 TodoLayered

A RESTful Todo API built with **ASP.NET Core** using a clean **Layered Architecture**. Supports full CRUD operations, validation, pagination, and structured error handling.

---

## 🏗️ Architecture

```
TodoLayered/
├── Todo.Entities/        # Domain models
├── Todo.DataAccess/      # EF Core, repositories
├── Todo.Business/        # DTOs, services, validation, mappers
└── Todo.WebAPI/          # Controllers, middlewares, program entry
```

The project follows a classic **4-layer architecture**:

| Layer | Responsibility |
|---|---|
| `Entities` | Plain domain models (`TodoItem`, `TodoQuery`) |
| `DataAccess` | Database context, EF Core migrations, repository pattern |
| `Business` | Business logic, DTO mapping, FluentValidation |
| `WebAPI` | HTTP layer, routing, middleware, dependency injection |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server or SQLite

### Run Locally

```bash
git clone https://github.com/AysunShirelizade1/TodoLayered.git
cd TodoLayered

dotnet restore
dotnet ef database update --project Todo.DataAccess --startup-project Todo.WebAPI
dotnet run --project Todo.WebAPI
```

API will be available at `https://localhost:5001`

---

## 📦 Features

- ✅ CRUD operations for Todo items
- ✅ DTO-based request/response separation
- ✅ FluentValidation for input validation
- ✅ Pagination support (`PagedResponseDto`)
- ✅ Custom middleware pipeline
- ✅ Repository pattern with interface abstraction
- ✅ Dependency injection via extension methods

---

## 🔌 API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/todos` | Get all todos (paginated) |
| `GET` | `/api/todos/{id}` | Get todo by ID |
| `POST` | `/api/todos` | Create a new todo |
| `PUT` | `/api/todos/{id}` | Update a todo |
| `DELETE` | `/api/todos/{id}` | Delete a todo |

---

## 🛠️ Tech Stack

- **ASP.NET Core 8**
- **Entity Framework Core**
- **FluentValidation**
- **AutoMapper / Manual Mapping**
- **SQL Server / SQLite**

---

## 📁 Key Files

```
Todo.Business/
├── DTOs/               # TodoCreateDto, TodoUpdateDto, TodoResponseDto, PagedResponseDto
├── Services/           # ITodoService, TodoService
├── Mappers/            # TodoMapper
└── Validations/        # TodoValidator

Todo.DataAccess/
├── Repositories/       # ITodoRepository, TodoRepository
└── Extensions/         # ServiceRegistration

Todo.WebAPI/
├── Controllers/        # TodoController
├── Middlewares/        # Custom middleware
└── Program.cs
```

---

## 👩‍💻 Author

**Aysun Şirəlizadə** — [@AysunShirelizade1](https://github.com/AysunShirelizade1)
