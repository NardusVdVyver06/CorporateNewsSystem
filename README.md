# Corporate News Management System

## Overview

This project is a full-stack web application consisting of:

* **.NET 10 Minimal REST API**
* **Blazor Frontend**

The application is designed to allow users to browse corporate news articles while providing administrators with the ability to create, update, and delete articles through secured API endpoints.

The backend follows a modern ASP.NET Core architecture using:

* .NET 10
* Minimal APIs
* Entity Framework Core
* SQLite
* Code-First Migrations
* JWT Authentication
* Role-Based Authorization

---

# Prerequisites

Before running the application, ensure the following software is installed.

## .NET 10 SDK

Download and install the .NET 10 SDK appropriate for your operating system:

https://dotnet.microsoft.com/en-us/download/dotnet/10.0

Verify the installation:

```bash
dotnet --version
```

---

## Entity Framework Core CLI

Install the EF Core command line tools:

```bash
dotnet tool install --global dotnet-ef
```

Verify the installation:

```bash
dotnet ef
```

---

# Restoring Packages

Restore all NuGet packages before running the project.

```bash
dotnet restore
```

---

# Database

The application uses **SQLite** together with **Entity Framework Core Code-First Migrations**.

The SQLite database will be created automatically after applying the migrations.

## Applying Existing Migrations

Run:

```bash
dotnet ef database update
```

---

## Creating a New Migration

After making changes to the Entity Framework models:

```bash
dotnet ef migrations add MigrationName
```

Apply the migration:

```bash
dotnet ef database update
```

---

# Running the API

Navigate to the Base project directory and run:

```bash
dotnet run --project News.Api
```

By default the API will be available at:

```text
http://localhost:5220
```

---

# Verifying the API

Once the API is running, browse to:

```text
http://localhost:5220/monitor
```

A successful response should return:

```text
News Api - Development
```

---

# Authentication

The API uses JWT Bearer Authentication.

Authenticate using the login endpoint to obtain a JWT token.

```text
POST /api/auth/login
```

The returned token should be included in subsequent requests using the Authorization header:

```text
Authorization: Bearer <your-jwt-token>
```

Administrative endpoints require the **Admin** role.

---

# Stopping the API

If running from the terminal:

```text
Ctrl + C
```

# Running the Unit Tests

Navigate to the Base project directory and run:

```bash
dotnet test
```

# Running the Blazor Website

Navigate to the Base project directory and run:

```bash
dotnet run --project News.Web
```

By default the Website will be available at:

```text
http://localhost:5190
```