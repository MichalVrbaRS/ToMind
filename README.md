# ToMind

ToMind is a Blazor Server app for managing To‑Do lists and Kanban boards with projects (tabs), assignees, and drag‑and‑drop.
It includes list sharing, a lightweight admin page, local storage for last opened list, and multiple light theme palettes plus a dark mode.

## Features
- Create **Todo** or **Kanban** lists with optional image and password protection
- Projects as tabs within a list (filter items by project)
- Assignees: type or select names; new names are added automatically
- Drag‑and‑drop reordering (Todo) and column moves (Kanban)
- Share link button in the top bar (copies to clipboard)
- Admin page to list, copy, and delete lists
- Theme toggle with multiple light palettes (cycle button in light mode)

## Getting started
**Requirements:** .NET 10 SDK  

```powershell
dotnet run --project Src\ToMind\ToMind.csproj
```

The SQLite database is stored at `Src\ToMind\App_Data\ToMind.db`.

## Admin access
Open `/admin` and use password (default: `todoadmin!1`).
For IIS, set `<add key="ToMind:AdminPassword" value="..."/>` in `web.config` under `<appSettings>`.
