# Student Panel

A Blazor WebAssembly front end talking to a minimal ASP.NET Core API — because sometimes you want the UI in the browser and the logic on the server, and you're not apologizing for it.

Browse students, open details, add new ones, assign courses, and mark favorites as "observed." The API uses in-memory data (no database drama for a UI-focused demo). The client shows real HTTP communication through a typed API client.

## What it does

- **Student list** — load all students on page init
- **Student details** — route-based loading by ID, course assignments, clipboard copy via JS interop
- **Create student** — `EditForm` + data annotation validation
- **Assign courses** — pick from available courses, prevent duplicates
- **Observed students** — client-side state shared across pages (counter in the nav menu)
- **404 page** — for routes that don't exist (it happens)

## Solution structure

| Project | Role |
|---------|------|
| `StudentPanel.Api` | Minimal API, in-memory `DataStore.cs` |
| `StudentPanel.Client` | Blazor WebAssembly UI |
| `StudentPanel.Shared` | Shared DTOs |

## Tech stack

- **Blazor WebAssembly**
- **ASP.NET Core Minimal API**
- **Typed `HttpClient`** wrapper (`StudentsApiClient.cs`)
- **JS Interop** for clipboard
- **ErrorBoundary** on the student list

## API endpoints

| Method | Route | Notes |
|--------|-------|-------|
| `GET` | `/api/students` | All students |
| `GET` | `/api/students/{id}` | Student + assigned courses |
| `POST` | `/api/students` | Create student |
| `GET` | `/api/courses` | Available courses |
| `POST` | `/api/students/{id}/courses` | Assign course to student |

## Run it locally

**Terminal 1 — API:**
```bash
dotnet run --project StudentPanel.Api
```
Runs at **http://localhost:5028**

**Terminal 2 — Blazor client:**
```bash
dotnet run --project StudentPanel.Client
```
Runs at **http://localhost:5067** — open this one in the browser.

The client calls the API over HTTP. Both need to be running.
