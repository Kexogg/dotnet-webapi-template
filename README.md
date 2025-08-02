# DotNet WebAPI template

Template for a .NET 9 WebAPI Project

Features:
- .NET 9
- Scalar and OpenAPI support
- Decoupled architecture
- EF Core

Project Structure:
- WebApi.Domain: Contains the domain entities and interfaces.
- WebApi.Application: Contains DTOs and interfaces for application services and repositories.
- WebApi.Infrastructure: Contains implementation for services and repositories, EF Core DbContext, and migrations.
- WebApi.Presentation: Contains controllers.
- WebApi.Startup: Contains entrypoint.
