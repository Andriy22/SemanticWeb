# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Structure

This is a semantic web project with multiple components:

### Main Application (Project/)
- **ASP.NET Core API** (.NET 9.0): `Project/SemanticWebProject/` - Web API with Entity Framework Core
- **React Frontend**: `Project/SemanticWebProject/client/` - TypeScript React app with Vite, Tailwind CSS, and shadcn/ui
- **Business Logic Layer**: `Project/BLL/` - Services and DTOs  
- **Data Access Layer**: `Project/DAL/` - Entity Framework models and DbContext

### Laboratory Work (lab1/, lab2/, mkr/)
- Semantic web exercises with RDF/TTL files and Python scripts
- SPARQL queries and ontology work

## Development Commands

### Backend (.NET API)
```bash
# From Project/SemanticWebProject/
dotnet build                    # Build the solution
dotnet run                      # Run the API server
dotnet ef database update       # Apply EF migrations
```

### Frontend (React)
```bash
# From Project/SemanticWebProject/client/
npm run dev                     # Start development server
npm run build                   # Build for production
npm run lint                    # Run ESLint
```

## Architecture

The main application follows a layered architecture:

1. **API Layer** (SemanticWebProject): Controllers expose REST endpoints
2. **Business Logic Layer** (BLL): Services handle business logic, DTOs for data transfer
3. **Data Access Layer** (DAL): Entity Framework models and database context

Key services:
- `IScientistsService`: Manages scientist data with external source integration
- `IDataSourceService`: Handles external data source communication

The frontend uses React Router for navigation with pages for scientist listing and details.

## Database

Uses Entity Framework Core with SQL Server. Connection string configured in `appsettings.json`.

## External Dependencies

- **Frontend**: React 18, Vite, Tailwind CSS, shadcn/ui components, React Router
- **Backend**: ASP.NET Core 9.0, Entity Framework Core, Scalar (API documentation)