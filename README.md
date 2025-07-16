# 🔐 FlexiQueryAPI - Secure Dynamic SQL Executor API

<p align="center">
  <a href="https://github.com/dufacoga/FlexiQueryAPI/issues"><img src="https://img.shields.io/github/issues/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/stargazers"><img src="https://img.shields.io/github/stars/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/network/members"><img src="https://img.shields.io/github/forks/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/commits/master"><img src="https://img.shields.io/github/last-commit/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/blob/master/CONTRIBUTING.md"><img src="https://img.shields.io/badge/contributions-welcome-brightgreen.svg"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/blob/master/LICENSE.txt"><img src="https://img.shields.io/github/license/dufacoga/FlexiQueryAPI"/></a>
</p>

**FlexiQueryAPI** is a secure and flexible REST API to perform basic SQL operations by sending generic DTOs, while the API constructs and executes the queries internally. Supports **SELECT**, **INSERT**, **UPDATE**, and **DELETE** using proper HTTP verbs.

---

## 🚀 Features

- ✅ Accepts generic DTOs for CRUD operations, internally transformed into SQL
- 🔒 Strong query validation: allows only whitelisted commands and prevents injection
- 🔌 Easily switch between **SQLite**, **SQL Server**, and **MySQL**
- 🧪 Swagger UI for testing queries with parameters
- 💡 Clean architecture with interfaces (`ISqlExecutor`, `IQueryBuilder`)

---

## 🧰 Tech Stack

- ASP.NET Core 8 Web API
- C# modern conventions
- Microsoft.Data.Sqlite / SqlClient / MySqlConnector
- Swagger / Swashbuckle for API docs

---

## ⚙️ Configuration

Example `appsettings.json`:

```json
{
  "DatabaseProvider": "SQLite",
  "ConnectionStrings": {
    "SqlServer": "Server=localhost;Database=SecureDb;Trusted_Connection=True;",
    "MySQL": "Server=localhost;Database=SecureDb;User=root;Password=yourpass;",
    "SQLite": "App_Data/secure.db"
  },
  "ApiKey": "supersecret123"
}
```

---

## 📂 Project Structure

```bash
📁 FlexiQueryAPI/
├── Controllers/
│   └── SqlQueryController.cs
├── Interfaces/
│   ├── ISqlExecutor.cs
│   └── IQueryBuilder.cs
├── Services/
│   ├── SqliteExecutor.cs
│   ├── SqlServerExecutor.cs
│   ├── MySqlExecutor.cs
│   └── QueryBuilder.cs
│   └── SqlSecurityValidator.cs
├── Dtos/
│   ├── Insert.cs
│   ├── Update.cs
│   ├── Delete.cs
│   └── Select.cs
├── Security/
│   ├── ApiKeyAuthenticationHandler.cs
│   └── ApiKeyValidator.cs
├── Utils/
│   └── QueryBuilder.cs
├── Program.cs
└── appsettings.json
```

---

## 🔒 Security

- ✅ Accepts only basic commands: SELECT, INSERT, UPDATE, DELETE
- ✅ Rejects multiple statements, comments, or dangerous keywords
- 🔐 Requires API key (`X-API-KEY` header)
- 🔄 All input values parameterized to avoid SQL injection

---

## 📦 Sample Request (POST /api/sqlquery/select)

```json
{
  "table": "Users",
  "columns": ["Id", "Name", "Username"],
  "where": {
    "RoleId": 2
  }
}
```

> 🔄 Similarly, use `/insert`, `/update`, `/delete` endpoints with appropriate DTOs.

---

## 🧪 Run Locally

```bash
git clone https://github.com/dufacoga/FlexiQueryAPI.git
cd FlexiQueryAPI
dotnet run
```

Visit: [http://localhost:PORT/swagger](http://localhost:PORT/swagger)

---

## 🧃 Sample Data

The included `example.db` SQLite file provides:

- 👤 200 Users
- 🛒 50 Shopping carts
- 🛍️ Products linked by foreign keys

---

## 🔄 Switch Database

Just update `"DatabaseProvider"` in `appsettings.json`.

---

## 📄 License

[MIT License](LICENSE)

---

## 👤 Author 

**Douglas Cortes**  
🔗 [LinkedIn](https://www.linkedin.com/in/dufacoga)  
🌐 [dufacoga.github.io](https://dufacoga.github.io)
