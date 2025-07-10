# 🔐 FlexiQueryAPI - Secure Dynamic SQL Executor API

<p align="center">
  <a href="https://github.com/dufacoga/FlexiQueryAPI/issues"><img src="https://img.shields.io/github/issues/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/stargazers"><img src="https://img.shields.io/github/stars/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/network/members"><img src="https://img.shields.io/github/forks/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/commits/master"><img src="https://img.shields.io/github/last-commit/dufacoga/FlexiQueryAPI"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/blob/master/CONTRIBUTING.md"><img src="https://img.shields.io/badge/contributions-welcome-brightgreen.svg"/></a>
  <a href="https://github.com/dufacoga/FlexiQueryAPI/blob/master/LICENSE.txt"><img src="https://img.shields.io/github/license/dufacoga/FlexiQueryAPI"/></a>
</p>

**FlexiQueryAPI** is a generic, secure, and pluggable REST API that allows execution of raw SQL queries using HTTP methods. It supports **SELECT**, **INSERT**, **UPDATE**, and **DELETE** operations mapped to appropriate HTTP verbs (`GET`, `POST`, `PUT`, `DELETE`) while enforcing query-type validation and basic protections.

---

## 🚀 Features

- ✅ RESTful API with support for CRUD operations via SQL strings
- 🔒 Secure execution: query-type validation and API key authentication
- 🔌 Plug-and-play database support: **SQL Server**, **MySQL**, **SQLite**
- 🧪 Integrated Swagger UI for interactive testing
- 🐳 Docker-ready & Azure App Services compatible

---

## 🧰 Tech Stack

- ASP.NET Core 8 Web API
- C# Modern practices
- Dapper
- Microsoft.Data.SqlClient / MySqlConnector / Microsoft.Data.Sqlite
- Swagger / Swashbuckle for API documentation

---

## ⚙️ Configuration

Set your desired database in `appsettings.json`:

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

To switch databases, simply change the `"DatabaseProvider"` value to one of:

- `"SqlServer"`
- `"MySQL"`
- `"SQLite"`

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
└── App_Data/
    └── example.db  # Preloaded if using SQLite
```

---

## 🔒 Security

FlexiQueryAPI includes several layers of protection:

- ✅ API key validation via `X-API-KEY` header
- ✅ Query whitelist: allows only `SELECT`, `INSERT`, `UPDATE`, `DELETE`
- 🚫 Blocks destructive operations: `DROP`, `TRUNCATE`, `ALTER`, `SHUTDOWN`
- 🧼 Sanitized logging to avoid leaking raw queries
- ⏱️ Optional: you can extend with pagination and timeout enforcement

---

## 📦 API Usage

### 📥 Example: `POST /api/sqlquery/execute`

**Headers:**
```
Content-Type: application/json
X-API-KEY: supersecret123
```

**Body:**
```json
{
  "query": "SELECT * FROM Users LIMIT 10"
}
```

**Response:**
```json
[
  {
    "id": 1,
    "firstName": "Emily",
    "email": "emily.johnson@example.com"
  }
]
```

> ℹ️ With the latest version, each HTTP method maps to its correct SQL command:
> - `GET` → SELECT
> - `POST` → INSERT
> - `PUT` / `PATCH` → UPDATE
> - `DELETE` → DELETE

---

## 🧪 Local Development

```bash
git clone https://github.com/dufacoga/FlexiQueryAPI.git
cd FlexiQueryAPI
dotnet run
```

Then open:📎 [`http://localhost:<port>/swagger`](http://localhost:<port>/swagger)

---

## 🧃 Sample Data (for SQLite)

If you're using the default `example.db`, it includes:

- 👤 200 Users
- 🛍️ 200 Products
- 🛒 50 Carts with relations

Example test query:

```json
{
  "query": "SELECT * FROM Users LIMIT 10"
}
```

---

## 🔄 Switching Database Provider

Update `appsettings.json`:

```json
"DatabaseProvider": "MySQL"
```

Ensure the connection string is correct and the database is reachable.

---

## 📄 License

Licensed under the [MIT License](LICENSE).

---

## 👤 Author

**Douglas Cortes**  
🔗 [LinkedIn](https://www.linkedin.com/in/dufacoga)  
🌐 [dufacoga.github.io](https://dufacoga.github.io)
