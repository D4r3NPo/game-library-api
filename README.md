# Game Library API

ASP.NET Core Web API for managing a personal game library.

This project lets you create, read, update, and delete games, with filtering, search, and validation.

> It was built as a backend portfolio project to demonstrate clean API design with C#, ASP.NET Core, Entity Framework Core, and SQLite.

---

## Features

- CRUD operations for games
- Search by title
- Filter by status
- Filter by platform
- Data validation
- SQLite database with Entity Framework Core
- Test coverage

---

## Tech Stack

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- OpenAPI

---

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
`GET /api/games?status=Playing`  
`GET /api/games?platform=PC`

---

## Validation Rules

- `Title` is required with length limited to 30
- `Rating` must be between `0` and `10`

---

## Getting Started

### Prerequisites

- .NET 10 SDK installed

### Run locally

```bash
git clone https://github.com/D4r3NPo/game-library-api.git
cd game-library-api
dotnet restore
dotnet ef database update --project GameLibraryApi
dotnet run --project GameLibraryApi