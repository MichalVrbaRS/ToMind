# ToMind --- Web App Specification

## 1. Purpose

ToMind is a lightweight web app for creating, using, and sharing **to-do
lists** and **Kanban-style boards**. Each list/board has a **unique
shareable URL** and can optionally be protected by a **password** for
viewing/editing.

------------------------------------------------------------------------

## 2. Tech Stack & Constraints

-   **.NET 10** (Blazor Server)
-   **SQLite** database
-   **Blazored.LocalStorage**
-   Hosting domain: `https://tomind.vrba.one`
-   Public list URL format: `https://tomind.vrba.one/{guid}`

------------------------------------------------------------------------

## 3. Core Concepts

### List Types

1.  **Traditional To-Do List**
    -   Add item
    -   Mark done/undone
    -   Delete item
    -   Optional ordering
2.  **Kanban Board**
    -   3 fixed columns:
        -   To Do
        -   In Progress
        -   Done
    -   Add card
    -   Move card between columns
    -   Delete card

### Project (Sub-Project)

-   Optional grouping label (e.g. Home, Work, Other)
-   A list may belong to zero or one project

### Access Mode

-   Public (no password)
-   Password protected

------------------------------------------------------------------------

## 4. Landing Behavior

When opening `https://tomind.vrba.one`:

-   If `lastOpenedListGuid` exists in local storage → redirect to that
    list
-   Otherwise → show **Create New List** screen

------------------------------------------------------------------------

## 5. Create List Screen

Fields:

-   Name (required)
-   Description (optional)
-   Image upload (optional)
-   Type (ToDo / Board)
-   Project (optional)
-   Password protection (optional)

After creation: - Redirect to `/{guid}` - Store `lastOpenedListGuid` in
local storage

------------------------------------------------------------------------

## 6. List Page `/{guid}`

### Shared Layout

-   App name
-   List name
-   Share button
-   Theme toggle (Light/Dark)
-   Optional image banner

### Password Gate

If password protected: - Prompt for password - "Remember me" checkbox -
Store token in local storage after successful login

------------------------------------------------------------------------

## 7. Data Model (SQLite)

### Lists

-   Id (GUID)
-   Name
-   Description
-   ImagePath
-   Type (0=ToDo, 1=Board)
-   ProjectName
-   PasswordHash
-   CreatedAtUtc
-   UpdatedAtUtc

### TodoItems

-   Id
-   ListId
-   Text
-   IsDone
-   SortOrder

### BoardCards

-   Id
-   ListId
-   Title
-   Description
-   Column (0=ToDo,1=InProgress,2=Done)
-   SortOrder

------------------------------------------------------------------------

## 8. Security

-   Passwords hashed (PBKDF2 / ASP.NET PasswordHasher)
-   Optional remember-me token stored per list
-   Token stored hashed in DB

------------------------------------------------------------------------

## 9. UI Requirements

-   Simple, modern UI
-   Dominant color: Blue
-   Light and Dark mode
-   Responsive design

------------------------------------------------------------------------

## 10. MVP Acceptance Criteria

-   Create list (ToDo or Board)
-   Unique shareable URL
-   Optional password protection
-   Remember-me functionality
-   Persistent storage in SQLite
-   Light/Dark theme toggle

------------------------------------------------------------------------

## 11. Out of Scope (MVP)

-   User accounts
-   Advanced permissions
-   Real-time collaboration
-   Version history
