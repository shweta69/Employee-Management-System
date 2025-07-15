
# Employee Management Web API

A simple role-based Employee Management System built using ASP.NET Core Web API and Entity Framework Core.

## 🔧 Tech Stack
- ASP.NET Core Web API (.NET 8) 
- Entity Framework Core (Code First)
- SQL Server
- JWT Authentication
- Swagger (API Testing)
- Global Exception Handling
- Local Exception Handling where ever needed

## 👥 Roles & Access
- **Admin**
  - Can manage everything (Users, Employees, Departments, Designations)
- **HR**
  - Can manage employees only (Create, Update, Delete)
- **Employee**
  - Can only view their own profile

## 🛠️ Setup Instructions

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Update the connection string in `appsettings.json` to your SQL Server.
4. Run the following commands in Package Manager Console:
   ```bash
   Add-Migration InitialCreate
   Update-Database
   ```
5. Run the application.

## 🔐 Testing Authenticated Endpoints

1. **Register a user** via `/api/auth/register` (Only Admin/HR can register users).
2. **Login** via `/api/auth/login` → Get a JWT token.
3. **Authorize in Swagger:**
   - Click **Authorize** in Swagger UI.
   - Paste token in the format: `Bearer <your-token>`.

## ✅ Sample Test Scenarios

- Admin can access all APIs.
- HR can manage employees but not users.
- Employee can view only their data using `/api/employees/{id}`.

## 🔎 Notes

- User and Employee are now linked using `UserId` (nullable FK in Employee).
- Validations and error handling are implemented.
- Soft delete is applied using `IsActive` flag.
