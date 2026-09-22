# URL Shortener API

A learning project built with ASP.NET Core and SQL Server. Create a short link for a destination URL, then open the link to redirect to that destination.

## Technologies

- .NET 10 and ASP.NET Core controllers
- Entity Framework Core with SQL Server
- OpenAPI and Scalar for API documentation

## Features

- Generates random, 16-character alphanumeric codes.
- Stores destination URLs and creation timestamps in SQL Server.
- Redirects visitors through `GET /url/{code}`.

## Run locally

You need the .NET 10 SDK and a running SQL Server instance. Run the commands below from the repository root.

### 1. Configure the connection

Set the connection string in the PowerShell session you will use to run the app:

```powershell
$env:ConnectionStrings__Connection = "Server=localhost;Database=Shortener;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
```

Replace the placeholders with your local credentials. The environment variable overrides the connection string in application settings. Local `appsettings.json` and `appsettings.Development.json` files are excluded from Git to keep local credentials out of the repository. Do not commit real passwords.

### 2. Create the database

The project does not yet include EF Core migrations or automatically create its database. For this initial version, execute the following script in SQL Server Management Studio or another SQL Server query tool:

```sql
CREATE DATABASE Shortener;
GO
USE Shortener;
GO
CREATE TABLE dbo.Url (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Code nvarchar(max) NOT NULL,
    Redirection nvarchar(max) NOT NULL,
    CreatedAt datetime2 NOT NULL
);
GO
```

Run this only for a new database. If `Shortener` already exists, use it and ensure that the table matches the schema above. The application login needs permission to read and insert rows.

### 3. Start the API

```powershell
dotnet restore
dotnet dev-certs https --trust
dotnet run --project url-shortener-net --launch-profile https
```

The HTTPS address is `https://localhost:7180`. In Development, open the interactive API documentation at `https://localhost:7180/scalar`.

## API examples

### Create a short link

`POST /url?redirection={encoded-destination}`

The destination is a query parameter, not a JSON request body.

```powershell
$destination = [uri]::EscapeDataString("https://example.com/articles?id=123")
$shortUrl = Invoke-RestMethod -Method Post -Uri "https://localhost:7180/url?redirection=$destination"
$shortUrl
```

Example response (`200 OK`, a URL string):

```text
https://localhost:7180/url/aB3dE5fG7hJ9kL2m
```

### Open a short link

Open the returned URL in a browser. `GET /url/{code}` responds with a `302` redirect to the saved destination.

## Project structure

```text
url-shortener-net/
  Controllers/   HTTP endpoints
  UseCases/      Code generation and short-link creation workflow
  Interface/     Repository contract
  Repositories/  Database operations
  Entities/      Stored URL model
  Mapper/        Entity creation
  Data/          Entity Framework database context
  Program.cs     Application configuration
```

## Current limitations

This is an initial learning version.

- Configure the public base URL; generated links currently use `https://localhost:7180`.
- Add EF Core migrations for database setup.
- Store timestamps in UTC without a fixed timezone adjustment.

