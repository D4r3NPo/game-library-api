# Game Library

Full-stack ASP.NET project for managing a personal game library.

The solution includes a Web API, a Blazor frontend, shared DTOs/enums, and API integration tests. It lets you create, read, update, and delete games, then view and edit them from the UI.

## Projects

- `GameLibraryApi`: ASP.NET Core Web API with Entity Framework Core and SQLite
- `GameLibraryFrontend`: Blazor web frontend for listing, creating, editing, deleting, and viewing basic statistics
- `GameLibrary.Shared`: shared DTOs and enums used by both backend and frontend
- `GameLibraryApi.Tests`: integration tests for the API

## Features

- CRUD operations for games
- Search by title
- Filter by status
- Filter by platform
- Shared contracts between API and frontend
- Blazor frontend for game management
- Simple statistics page
- SQLite database with Entity Framework Core
- API integration tests

## Tech Stack

- C#
- ASP.NET Core Web API
- Blazor
- Entity Framework Core
- SQLite
- OpenAPI
- xUnit

## API Endpoints

### Get all games
`GET /api/games`

### Get a game by id
`GET /api/games/{id}`

### Create a game
`POST /api/games`

### Update a game
`PUT /api/games/{id}`

### Delete a game
`DELETE /api/games/{id}`

### Search / filter examples
`GET /api/games?title=zelda`  
`GET /api/games?status=InProgress`  
`GET /api/games?platform=Windows`

## Validation Rules

- `Title` is required with length limited to 30
- `Rating` must be between `0` and `10`

## Getting Started

### Prerequisites

- .NET 10 SDK

### Run the API

```bash
dotnet restore
dotnet ef database update --project GameLibraryApi
dotnet run --project GameLibraryApi
```

### Run the frontend

Set `ApiBaseUrl` in `GameLibraryFrontend/appsettings.json` or `GameLibraryFrontend/appsettings.Development.json`, then run:

```bash
dotnet run --project GameLibraryFrontend
```

### Run tests

```bash
dotnet test GameLibraryApi.sln
```

## Current Limitations

- Frontend error handling is still minimal and needs more user feedback for failed API requests.
