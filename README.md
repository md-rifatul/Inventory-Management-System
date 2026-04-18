# Inventory Management System

An ASP.NET Core MVC application for managing products, categories, suppliers, stock flow, purchase orders, sales orders, and online payment flow.

## Tech Stack

- .NET 8 (`net8.0`)
- ASP.NET Core MVC
- Entity Framework Core 8
- SQL Server (local `SQLEXPRESS` by default)
- ASP.NET Core Identity (authentication and role-based authorization)
- Stripe Checkout (order payment integration)
- AutoMapper

## Solution Structure

The solution follows a layered architecture:

- `Inventory.Web` - Presentation layer (MVC controllers, views, startup configuration)
- `Inventory.Application` - Business logic, services, interfaces, and view models
- `Inventory.Domain` - Core entities and enums
- `Inventory.Infrastructure` - EF Core data access, repositories, migrations, identity-related infrastructure

## Main Features

- User registration and login
- Role-based access control (`Admin`, `User`)
- Category management
- Product management
- Supplier management
- Stock transaction tracking
- Purchase order management
- Sales order management
- Stripe payment session creation and payment status update

## Prerequisites

Install the following before running the project:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (Express is fine)
- Visual Studio 2022 or VS Code/Cursor with C# support
- (Optional) [EF Core CLI tools](https://learn.microsoft.com/ef/core/cli/dotnet)

Install EF CLI globally if needed:

```bash
dotnet tool install --global dotnet-ef
```

## Configuration

The app reads configuration from `Inventory.Web/appsettings.json` and environment-specific files.

### 1) Database Connection String

Default connection string in `Inventory.Web/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Update this value based on your SQL Server instance.

### 2) Stripe Settings

Add Stripe settings in `Inventory.Web/appsettings.json` (or user-secrets/environment variables):

```json
"Stripe": {
  "SecretKey": "sk_test_your_secret_key",
  "WebhookSecret": "whsec_your_webhook_secret"
}
```

Also confirm:

```json
"AppSettings": {
  "BaseUrl": "https://localhost:44320"
}
```

Set `BaseUrl` to your app URL used by Stripe success/cancel redirects.

## Database Setup

From the solution directory (`Inventory.Web` folder that contains `Inventory.Web.sln`):

```bash
dotnet restore
dotnet ef database update --project Inventory.Infrastructure --startup-project Inventory.Web
```

> The repository already includes EF Core migrations in `Inventory.Infrastructure/Migrations`.

## Run the Application

From the same solution directory:

```bash
dotnet build
dotnet run --project Inventory.Web
```

Then open the app in your browser using the URL shown in the terminal (typically `https://localhost:<port>`).

## Default Seeded Roles and Admin

At startup, the app seeds:

- Roles: `Admin`, `User`
- Default admin user from seeder:
  - Email: `admin@gmail.com`
  - Password: `Admin@123`

You should change these values for production use.

## Useful Commands

Run from the solution directory:

```bash
# Build everything
dotnet build Inventory.Web.sln

# Run web project
dotnet run --project Inventory.Web

# Apply migrations
dotnet ef database update --project Inventory.Infrastructure --startup-project Inventory.Web
```

## Troubleshooting

- **SQL connection errors**
  - Verify `DefaultConnection` and SQL Server instance name.
- **Stripe errors**
  - Confirm `Stripe:SecretKey`, `Stripe:WebhookSecret`, and `AppSettings:BaseUrl`.
- **Migration command fails**
  - Ensure `dotnet-ef` is installed and run command from the solution directory.
- **HTTPS/SSL issues in local**
  - Trust dev certificate:
    ```bash
    dotnet dev-certs https --trust
    ```

## Security Notes

- Do not commit real Stripe keys or production secrets to source control.
- Prefer environment variables or Secret Manager for sensitive configuration.

## License

No license file is currently defined in this repository. Add a `LICENSE` file if you plan to distribute this project.