# Copilot instructions for ToMind

## Build
- `dotnet build .\Src\ToMind.slnx`

## Run
- `dotnet run --project .\Src\ToMind\ToMind.csproj`

## High-level architecture
- .NET 10 Blazor Server app; startup is in `Src\ToMind\Program.cs` using Razor Components with interactive server render mode.
- HTML shell and static assets are wired in `Components\App.razor`; global styles live in `wwwroot\app.css` and `ToMind.styles.css`.
- Routing is defined in `Components\Routes.razor`, with pages under `Components\Pages` and layout components under `Components\Layout`.
- Product requirements and intended data model are documented in `Specs\ToMind_Web_App_Specification.md`.

## Key conventions
- Razor pages live in `Components\Pages` and use `@page` for routes; shared layout is `Components\Layout\MainLayout.razor`.
- Component-scoped assets are co-located with the component (`.razor.css`, `.razor.js`) as seen in `ReconnectModal.razor`.
- 404s are routed to `/not-found` via `UseStatusCodePagesWithReExecute` in `Program.cs` and handled by `Pages\NotFound.razor`.
