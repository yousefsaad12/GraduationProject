# GraduationProject

A full-stack ASP.NET Core Web API project for managing doctors and users, including authentication, JWT token handling, and CRUD operations. The project is designed with clean architecture principles, using Entity Framework Core, Identity, and best practices for maintainability and testability.

---

## Table of Contents

- [Features](#features)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Setup](#setup)
  - [Database Migration](#database-migration)
  - [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
  - [Doctor Endpoints](#doctor-endpoints)
  - [User Endpoints](#user-endpoints)
- [API Endpoints in Depth](#api-endpoints-in-depth)
- [Authentication & Authorization](#authentication--authorization)
- [Unit Testing](#unit-testing)
- [Extending the Project](#extending-the-project)
- [Troubleshooting](#troubleshooting)
- [License](#license)

---

## Features

- **Doctor Management**: Add, update, delete, and list doctors.
- **User Authentication**: Register, login, and delete users.
- **JWT Token**: Secure endpoints with JWT-based authentication.
- **Validation**: Strong model validation for all requests.
- **Unit Testing**: xUnit and Moq-based unit tests for all services.
- **Swagger**: API documentation and testing UI.

---

## Project Structure

```
GraduationProject/
│
├── Controllers/           # API controllers (Doctor, User)
├── Data/                  # Entity Framework DbContext
├── Dtos/                  # Data Transfer Objects (DTOs)
├── InterFaces/            # Service interfaces
├── Mapping/               # Mapping helpers (DTO <-> Model)
├── Models/                # Entity models (Doctor, User)
├── Services/              # Service implementations
├── Utilities/             # Utility classes (e.g., ServicesResult)
├── Migrations/            # EF Core migrations
├── Program.cs             # Main entry point and DI setup
├── appsettings.json       # Configuration (DB, JWT, etc.)
│
└── GraduationProject.Tests/ # xUnit test project
    ├── DoctorServicesTests.cs
    ├── AuthServicesTests.cs
    └── TokenServicesTests.cs
```

---

## Technologies Used

- **.NET 9 / .NET 8 / .NET 6** (update as per your target)
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **Microsoft Identity**
- **JWT (JSON Web Token)**
- **xUnit** (unit testing)
- **Moq** (mocking for tests)
- **Swagger** (OpenAPI UI)

---

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or change connection string for your DB)
- (Optional) Visual Studio or VS Code

### Setup

1. **Clone the repository:**
   ```sh
   git clone <your-repo-url>
   cd GraduationProject
   ```

2. **Configure the database and JWT:**
   - Edit `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=GraduationProjectDb;Trusted_Connection=True;"
     },
     "JWT": {
       "Key": "YourSuperSecretKeyHere",
       "Issuer": "YourIssuer",
       "Audience": "YourAudience"
     }
     ```

### Database Migration

1. **Apply migrations and create the database:**
   ```sh
   dotnet ef database update
   ```

   > If you need to create a migration:
   > ```sh
   > dotnet ef migrations add InitialCreate
   > ```

### Running the Application

1. **Start the API:**
   ```sh
   dotnet run
   ```
2. **Open Swagger UI:**
   - Navigate to `http://localhost:5000/swagger` (or the port shown in your console).

---

## API Endpoints

### Doctor Endpoints

| Method | Route                      | Description                | Auth Required |
|--------|----------------------------|----------------------------|--------------|
| GET    | `/api/Doctor/GetAllDoctor` | List all doctors           | No           |
| POST   | `/api/Doctor/AddDoctor`    | Add a new doctor           | Yes          |
| PUT    | `/api/Doctor/UpdateDoctor` | Update doctor by ID        | Yes          |
| DELETE | `/api/Doctor/DeleteDoctor` | Delete doctor by ID        | Yes          |

### User Endpoints

| Method | Route                      | Description                | Auth Required |
|--------|----------------------------|----------------------------|--------------|
| POST   | `/api/User/register`       | Register a new user        | No           |
| POST   | `/api/User/login`          | Login and get JWT token    | No           |
| DELETE | `/api/User/delete-User`    | Delete user by email       | Yes          |

## API Endpoints in Depth

### Doctor Endpoints

#### GET `/api/Doctor/GetAllDoctor`
- **Description:** Retrieves a list of all doctors.
- **Request Body:** None.
- **Response:**
  - **Status:** 200 OK
  - **Body:** Array of doctor objects.
    ```json
    [
      {
        "id": 1,
        "name": "Dr. John Doe",
        "specialization": "Cardiology",
        "email": "john.doe@example.com",
        "phoneNumber": "1234567890",
        "country": "USA",
        "location": "New York"
      },
      ...
    ]
    ```

#### POST `/api/Doctor/AddDoctor`
- **Description:** Adds a new doctor.
- **Request Body:**
  ```json
  {
    "name": "Dr. John Doe",
    "specialization": "Cardiology",
    "email": "john.doe@example.com",
    "phoneNumber": "1234567890",
    "country": "USA",
    "location": "New York"
  }
  ```
- **Response:**
  - **Status:** 201 Created
  - **Body:**
    ```json
    {
      "success": true,
      "message": "Doctor added successfully."
    }
    ```

#### PUT `/api/Doctor/UpdateDoctor`
- **Description:** Updates an existing doctor by ID.
- **Request Body:**
  ```json
  {
    "id": 1,
    "name": "Dr. John Doe Updated",
    "specialization": "Neurology",
    "email": "john.doe.updated@example.com",
    "phoneNumber": "9876543210",
    "country": "Canada",
    "location": "Toronto"
  }
  ```
- **Response:**
  - **Status:** 200 OK
  - **Body:**
    ```json
    {
      "success": true,
      "message": "Doctor updated successfully."
    }
    ```

#### DELETE `/api/Doctor/DeleteDoctor`
- **Description:** Deletes a doctor by ID.
- **Request Body:** None.
- **Response:**
  - **Status:** 200 OK
  - **Body:**
    ```json
    {
      "success": true,
      "message": "Doctor has been deleted successfully."
    }
    ```

### User Endpoints

#### POST `/api/User/register`
- **Description:** Registers a new user.
- **Request Body:**
  ```json
  {
    "userName": "testuser",
    "email": "test@example.com",
    "password": "Test123!"
  }
  ```
- **Response:**
  - **Status:** 201 Created
  - **Body:**
    ```json
    {
      "succeeded": true,
      "errors": []
    }
    ```

#### POST `/api/User/login`
- **Description:** Logs in a user and returns a JWT token.
- **Request Body:**
  ```json
  {
    "email": "test@example.com",
    "passWord": "Test123!"
  }
  ```
- **Response:**
  - **Status:** 200 OK
  - **Body:**
    ```json
    {
      "user": {
        "userName": "testuser",
        "email": "test@example.com"
      },
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    }
    ```

#### DELETE `/api/User/delete-User`
- **Description:** Deletes a user by email.
- **Request Body:** None.
- **Response:**
  - **Status:** 200 OK
  - **Body:**
    ```json
    {
      "success": true,
      "data": "test@example.com"
    }
    ```

---

## Authentication & Authorization

- **JWT Bearer Authentication** is used.
- After login, include the returned token in the `Authorization` header for protected endpoints:
  ```
  Authorization: Bearer <your-jwt-token>
  ```

---

## Unit Testing

- All core services are covered by unit tests using **xUnit** and **Moq**.
- To run tests:
  ```sh
  cd GraduationProject.Tests
  dotnet test
  ```
- Test files:
  - `DoctorServicesTests.cs`
  - `AuthServicesTests.cs`
  - `TokenServicesTests.cs`

---

## Extending the Project

- **Add new models** in `Models/` and update `ApplicationDb`.
- **Add new endpoints** in `Controllers/`.
- **Add new services** in `Services/` and register them in `Program.cs`.
- **Add new tests** in `GraduationProject.Tests/`.

---

## Troubleshooting

- **Database errors:** Check your connection string and ensure SQL Server is running.
- **JWT errors:** Ensure your `JWT:Key`, `Issuer`, and `Audience` are set and match in both appsettings and your client.
- **Tests not running:** Ensure all NuGet packages are restored (`dotnet restore`), and you are in the correct directory.

---

## License

This project is for educational purposes. You may use, modify, and distribute it as needed.

---

**Enjoy building with ASP.NET Core!**

If you need more details or want to add deployment instructions, Docker, or CI/CD, let me know! 