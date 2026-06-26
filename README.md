# Corporate News Management System

## Overview

This project is a full-stack web application consisting of:

- **News.Api** - A .NET 10 Minimal REST API
- **News.Web** - A Blazor Server web application
- **News.Shared** - Shared models used by both the API and frontend
- **News.Tests** - Unit tests for the API

The application allows users to browse corporate news articles while providing administrators with secure endpoints to create, update and delete articles.

---

# Technology Stack

## Backend

- .NET 10
- ASP.NET Core Minimal APIs
- Entity Framework Core
- SQLite
- JWT Authentication
- Role-Based Authorization

## Frontend

- Blazor Server (.NET 10)
- Bootstrap

## Testing

- Unit tests using **xUnit**
- Integration tests using **ASP.NET Core's `WebApplicationFactory`**
- End-to-end HTTP endpoint validation using `HttpClient`

---

# Solution Structure

```
CorporateNews.sln
│
├── News.Api
│   REST API
│
├── News.Web
│   Blazor Frontend
│
├── News.Shared
│   Shared Requests, Responses and Models
│
└── News.Tests
    Unit Tests
```

---

# Prerequisites

Please install the following before running the solution.

## .NET 10 SDK

Download from:

https://dotnet.microsoft.com/download

Verify installation:

```bash
dotnet --version
```

---

## Entity Framework Core CLI

Install globally:

```bash
dotnet tool install --global dotnet-ef
```

Verify installation:

```bash
dotnet ef
```

---

# Restoring Packages

From the solution root:

```bash
dotnet restore
```

---

# Database

The project uses SQLite together with Entity Framework Core Code-First Migrations.

## Apply Existing Migrations

```bash
dotnet ef database update --project News.Api
```

This will automatically create the SQLite database.

---

## Creating New Migrations

After modifying an Entity:

```bash
dotnet ef migrations add MigrationName --project News.Api
```

Then update the database:

```bash
dotnet ef database update --project News.Api
```

---

# Running the API

Navigate to the solution root and run:

```bash
dotnet run --project News.Api
```

The API will start on:

```
http://localhost:5220
```

---

# Verify the API

Navigate to:

```
http://localhost:5220/monitor
```

Expected response:

```
News Api - Development
```

---

# Running the Blazor Frontend

In a separate terminal:

```bash
dotnet run --project News.Web
```

The website will be deployed to:

```
http://localhost:7072
```

Open the displayed URL in your browser.

> **Note**
>
> The API must be running before starting the Blazor application.

---

# Running Unit Tests

From the solution root:

```bash
dotnet test
```

or

```bash
dotnet test News.Tests
```

---

# Authentication

The API uses JWT Bearer Authentication.

Login:

```
POST /api/auth/login
```

The returned JWT should be supplied in the Authorization header.

```
Authorization: Bearer {token}
```

Administrative endpoints require the **Admin** role.

---

# Stopping the Applications

If running from the terminal:

```
Ctrl + C
```

---

# Continuous Integration

A GitHub Actions workflow is included.

Every Pull Request automatically:

- Restores NuGet packages
- Builds the solution
- Ensures the project compiles successfully

---

# Project Highlights

- Minimal APIs
- RESTful endpoint design
- JWT Authentication
- Role-Based Authorization
- Entity Framework Core Code-First Migrations
- SQLite Database
- Blazor Server Frontend
- Shared Contracts Library
- Unit Tests
- GitHub Actions CI Pipeline

---
