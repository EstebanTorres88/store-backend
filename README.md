# store-backend

ASP.NET Core Web API for a store catalog. It exposes CRUD operations for products, persists them in SQL Server through Entity Framework Core, and is organized in layers (API, Facade, DomainService, Infrastructure, Domain, DTO, Exceptions).

## Requirements

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) or later
- SQL Server instance (local or Azure SQL) reachable from the machine running the API
- `dotnet-ef` CLI tool if you need to apply or create migrations:

```bash
dotnet tool install --global dotnet-ef
```

## Project layout

```
src/
├── store-backend.Api/             HTTP controllers, request/response models, mappers, Program.cs
├── store-backend.Facade/          Public entry point of the business logic (IProductFacade)
├── store-backend.DomainService/   Business rules (IProductService)
├── store-backend.Infrastructure/  EF Core DbContext, repositories, migrations
├── store-backend.Domain/          Entities (Product)
├── store-backend.Dto/             Data transfer objects (ProductDTO)
└── store-backend.Exceptions/      Custom exceptions (ResourceNotFoundException)
```

A request flows through:

```
Controller -> Facade -> DomainService -> Repository -> DbContext -> SQL Server
```

Each layer only knows the layer directly below it. The Facade converts domain entities to DTOs, so controllers never touch EF Core types.

## Configuration

The connection string is read from `ConnectionStrings:DefaultConnection`. Set it in `src/store-backend.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<host>;Initial Catalog=<database>;User ID=<user>;Password=<password>;Encrypt=True;"
  }
}
```

Do not commit real credentials. Use [user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) or environment variables (`ConnectionStrings__DefaultConnection`) instead.

CORS is configured in `appsettings.json` under `Cors:AllowedOrigins`. Currently allowed: `http://localhost:5001` and `http://localhost:5173`.

## Run

```bash
# restore and build
dotnet build store-backend.slnx

# apply database migrations
dotnet ef database update --project src/store-backend.Infrastructure --startup-project src/store-backend.Api

# start the API (http profile, port 5001)
dotnet run --project src/store-backend.Api --launch-profile http
```

Swagger UI is available in the Development environment at `http://localhost:5001/swagger`.

## Endpoints

Base route: `/api/products`

| Method | Route              | Description                  | Success response |
| ------ | ------------------ | ---------------------------- | ---------------- |
| GET    | `/api/products`        | List all products        | `200 OK`         |
| GET    | `/api/products/{id}`   | Get one product by its public `Guid` | `200 OK` / `404 Not Found` |
| POST   | `/api/products`        | Create a product         | `201 Created`    |
| DELETE | `/api/products/{id}`   | Delete a product by its public `Guid` | `204 No Content` / `404 Not Found` |

Create request body:

```json
{
  "productResourceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Example product"
}
```

Both fields are required; `name` is limited to 50 characters.

`Product` uses an internal `int` primary key (`ProductId`) and a public `Guid` (`ProductResourceId`) exposed to clients.

## Migrations

```bash
dotnet ef migrations add <MigrationName> --project src/store-backend.Infrastructure --startup-project src/store-backend.Api
```

Existing migration: `InitialCreate` (creates the `Product` table).
