# 🤝 Contributing to FlexiQueryAPI

Thanks for taking a moment to contribute! Whether you're reporting a bug, suggesting an improvement, or submitting a pull request — your effort is appreciated.

**FlexiQueryAPI** is a secure and generic API built with **ASP.NET Core 8**, designed to support **dynamic SQL execution** across multiple database providers (SQL Server, MySQL, SQLite).

---

## 🐞 Found a Bug or Have a Feature Request?

If you discover an issue or have a suggestion to improve the project, please [open an issue](https://github.com/dufacoga/FlexiQueryAPI/issues). Include relevant details, logs (if safe), and reproduction steps if possible.

---

## 🔧 How to Submit a Pull Request

1. Discuss the change in an open issue (optional but recommended).
2. Fork this repository to your GitHub account.
3. Create a new branch:
   ```
   git checkout -b fix/sql-timeout-issue
   ```
4. Make your changes in Visual Studio or your preferred IDE.
5. Commit using clear, descriptive messages:
   ```
   git commit -m "Add timeout handling to SQL Server executor"
   ```
6. Push your branch and open a pull request targeting `main`.

---

## 🛠️ Development Setup

This is an ASP.NET Core Web API project. To run it locally:

1. Ensure you have [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed.
2. Clone the repository and navigate into the folder.
3. Run the app:
   ```bash
   dotnet run
   ```
4. Open your browser to:
   [https://flexiqueryapi.net/swagger](https://flexiqueryapi-dpdpewd4dzhfccau.centralus-01.azurewebsites.net/swagger)

If using SQLite, a preloaded `example.db` with 200 users, 200 products, and 50 carts is included.

---

## 🛡️ Code Style & Security Guidelines

To keep the API safe and stable, please follow these rules:

- ✅ Use the existing `SqlSecurityValidator` when changing query handling.
- 🚫 Never allow dangerous SQL operations (`DROP`, `TRUNCATE`, `ALTER`, etc.).
- 🧼 Do **not** log raw user queries — always sanitize them before logging.
- ✅ Follow the current structure for services and dependency injection.
- ✅ Use `appsettings.json` and environment variables for config settings.

---

## 🧩 Adding Support for a New Database

To contribute support for another DBMS (e.g., PostgreSQL):

1. Implement a new class that inherits from `ISqlExecutor`.
2. Add necessary connection string entry in `appsettings.json`.
3. Update `Program.cs` to support the new `DatabaseProvider` option.
4. Extend the security validator if needed.

---

## 🧭 Branch Naming Suggestions

- `fix/connection-string`
- `feature/add-postgresql-support`
- `docs/improve-readme`
- `refactor/validator-structure`

---

## 🤝 Code of Conduct

This project follows a basic rule: **be respectful and constructive**. Whether you’re writing a PR, reviewing code, or opening an issue — be kind and helpful.

If needed, refer to the [Code of Conduct](CODE_OF_CONDUCT.md).

---

Thanks for helping make FlexiQueryAPI better!
