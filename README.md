# 🔐 FlexiQueryAPI - Secure Dynamic SQL Executor API

<p align="center">
  <a href="https://github.com/dufacoga/FlexiQueryAPI/issues"><img src="https://img.shields.io/github/issues/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/stargazers"><img src="https://img.shields.io/github/stars/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/network/members"><img src="https://img.shields.io/github/forks/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/commits/master"><img src="https://img.shields.io/github/last-commit/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/blob/master/contributing.md"><img src="https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/blob/master/LICENSE"><img src="https://img.shields.io/github/license/dufacoga/FlexiQueryAPI"/></a>
</p>

FlexiQueryAPI is a generic, robust, and highly secure API that allows you to execute dynamic SQL queries via a single endpoint, with support for multiple database engines: **SQL Server**, **MySQL**, and **SQLite**.

---

## 🚀 Features

- ✅ Generic API with support for SELECT, INSERT, UPDATE, and DELETE
- 🔒 Built-in security with query validation and API Key authentication
- ⚙️ Pluggable database engines: switch between SQL Server, MySQL, and SQLite via config
- 🧪 Swagger integration for interactive testing
- 🔧 Azure App Services deployment ready

---

## 🧰 Technologies Used

- ASP.NET Core 8 Web API
- Modern C#
- MySqlConnector, Microsoft.Data.Sqlite, and Microsoft.Data.SqlClient
- Swashbuckle/Swagger for API documentation

---

## ⚙️ Database Configuration

The provider is set in `appsettings.json`:

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

Change the value of `DatabaseProvider` to `SqlServer`, `MySQL`, or `SQLite` to switch engines.

---

## 📂 Project Structure

```bash
📁 FlexiQueryAPI/
├── Controllers/
│   └── SqlQueryController.cs
├── Services/
│   ├── ISqlExecutor.cs
│   ├── SqlServerExecutor.cs
│   ├── MySqlExecutor.cs
│   ├── SqliteExecutor.cs
│   └── SqlSecurityValidator.cs
├── Security/
│   ├── ApiKeyAuthenticationHandler.cs
│   └── ApiKeyValidator.cs
├── Config/
│   └── DbProviderOptions.cs
├── Program.cs
├── appsettings.json
├── App_Data/
│   ├── example.db (only if using SQLite)
```

---

## 🔒 Security Validations

The API includes protections against abuse:

- ✅ API Key authentication (`X-API-KEY` header)
- ✅ Command whitelist: only `SELECT`, `INSERT`, `UPDATE`, and `DELETE` allowed
- ✅ Blocks destructive queries: `DROP`, `TRUNCATE`, `ALTER`, `SHUTDOWN`
- ✅ Log sanitization: never logs raw queries
- ✅ Optional: extend to add pagination or timeouts for heavy SELECTs

---

## 📦 Example API Request

### Endpoint:

```http
POST /api/sqlquery/execute
```

### Headers:

```
Content-Type: application/json
X-API-KEY: supersecret123
```

### Body:

```json
{
  "query": "SELECT * FROM Users"
}
```

### Successful Response:

```json
[
  {
    "id": 1,
    "firstName": "Emily",
    "email": "emily.johnson@example.com",
    ...
  }
]
```

---

## 🧪 Running Locally

### 🧃 Sample Data

If you're using SQLite, the included `example.db` file comes preloaded with:

- 👤 200 users
- 🛒 200 products
- 🧺 50 shopping carts (with product relations)

This allows you to immediately test queries like:

```json
{
  "query": "SELECT * FROM Users LIMIT 10"
}
```

```bash
git clone https://github.com/dufacoga/FlexiQueryAPI.git
cd FlexiQueryAPI
dotnet run
```

Visit Swagger UI at:

[https://flexiqueryapi.net/swagger](https://flexiqueryapi-dpdpewd4dzhfccau.centralus-01.azurewebsites.net/swagger)

---

## 🧩 Switching Database Provider

Simply edit `appsettings.json`:

```json
"DatabaseProvider": "MySQL"
```

Make sure the matching connection string is configured correctly.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 👤 Author

**Douglas Cortes**\
💼 [LinkedIn](https://www.linkedin.com/in/dufacoga)\
🌐 [dufacoga.github.io](https://dufacoga.github.io)
