# Recipe Portal (Przepisy)

This is a coursework project, developed in Polish as part of academic requirements.

## Description

This is a web application for sharing and managing cooking recipes. The platform allows users to add, review, rate, and browse recipes. It features a **public portal** for users and an **admin intranet** for managing content.

## Technologies Used

- ASP.NET Core (Razor Pages)
- Entity Framework Core (Code First)
- DaisyUI (Tailwind CSS)
- MS SQL Server
- HTML, CSS, JavaScript

## Features

### Portal (for users)
- User profiles with avatar and bio
- Adding new recipes (with admin approval)
- Rating and reviewing recipes
- Viewing ingredients and details
- Favorite recipes
- Polish UI

### Intranet (for admin)
- Accepting/rejecting user-submitted recipes
- Managing users, roles, reviews, and ratings
- CMS: editable pages and news
- Professional CRUD interface for all entities

## Project Structure

- `Przepisy.Data`: Contains EF Core models and context
- `Przepisy.PortalWWW`: Public-facing portal (user interface)
- `Przepisy.Intranet`: Admin panel for managing data

## Entities Overview

Includes but is not limited to:

- `Przepis` (Recipe)
- `Skladnik` (Ingredient)
- `Recenzja` (Review)
- `Ocena` (Rating)
- `Uzytkownik` (User)
- `Kuchnia` (Cuisine)
- `GrupaPrzepisu` (Meal type)
- `Strona`, `Aktualnosc` (CMS content)

## Screenshots

*To be added soon!*

## Note

This project is entirely in Polish and is designed to meet specific coursework requirements for university.


