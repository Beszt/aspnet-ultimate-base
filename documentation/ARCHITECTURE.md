# ARCHITECTURE.md

## 1. Overview

AspNetUltimateBase is a ready-to-code .NET 10 Web API template that follows Clean Architecture and CQRS. It ships opinionated defaults (JWT auth, MediatR, FluentValidation, EF Core, NLog, Docker, GitHub Actions) so new services can start from a consistent foundation.

## 2. Solution layout

```
AspNetUltimateBase.Domain/         # Entities and interfaces
AspNetUltimateBase.Application/    # CQRS commands/queries, DTOs, validation, mappings
AspNetUltimateBase.Infrastructure/ # EF Core persistence, repositories, migrations, seeders
AspNetUltimateBase.Presentation/   # API host, controllers, auth, swagger, configuration
AspNetUltimateBase.*.Tests/        # xUnit test projects per layer
```

Key points:
- `Domain` holds pure models and contracts (no infrastructure).
- `Application` orchestrates business logic with MediatR and FluentValidation; AutoMapper profiles live here.
- `Infrastructure` wires EF Core, repositories, migrations, and database seeders.
- `Presentation` bootstraps ASP.NET Core, authentication/authorization, Swagger UI, and exposes controllers.
- Tests use in-memory EF Core contexts to validate repositories and handlers without external dependencies.

## 3. Core flows

- **Authentication/Authorization**: JWT bearer auth with issuer/key/expiry from configuration; role-based authorization through `[Authorize(Roles = "...")]`.
- **Request handling**: Controllers delegate to MediatR commands/queries, validated by FluentValidation before hitting repositories.
- **Persistence**: EF Core `FoodBarDbContext` with migrations under `Infrastructure/Migrations`; repositories encapsulate data access.
- **Seeding**: `IPopulator` seeds roles, users, and sample products on startup to keep the demo usable.
- **Logging**: NLog configured via `nlog.config` (copy from `nlog.config.example`), supports file/email targets.

## 4. Configuration & environments

- Settings come from `appsettings.json` / `appsettings.<Environment>.json` in `AspNetUltimateBase.Presentation`.
- Environment variables override JSON keys using double-underscore naming, e.g. `ConnectionStrings__AspNetUltimateBase`, `Jwt__Key`, `Jwt__Issuer`, `Jwt__ExpireInDays`.
- Development uses the `appsettings.Development.json` defaults; production should rely on environment variables or a mounted `appsettings.json`.
- NLog reads from `nlog.config`; provide real targets/secrets in your environment or mount a custom file in Docker.

## 5. Database

- The API expects a SQL Server connection string under `ConnectionStrings.AspNetUltimateBase`.
- Apply migrations via `dotnet ef database update` (from `AspNetUltimateBase.Presentation`) or let `dotnet publish` generate runtime assets for container builds.
- Tests rely on EF Core InMemory; no external database is needed for CI.

## 6. Docker

- Multi-stage Dockerfile builds the API and runs it on the `mcr.microsoft.com/dotnet/aspnet:10.0` runtime image.
- Configure containers via environment variables (same names as appsettings keys) to avoid rebuilding images for different environments.
- Example: pass `ConnectionStrings__AspNetUltimateBase` and `Jwt__Key` when running the container to point at your SQL Server and secrets.

## 7. CI/CD conventions

- Default branch: `develop`; feature branches merge back via PR with CI checks (lint/build/test).
- Release workflow (manual dispatch) bumps `ProgramInfo.cs`, updates `documentation/CHANGELOG.md`, builds/tests/publishes, tags `vX.Y.Z`, zips artifacts, and optionally pushes Docker images.
- GitHub rulesets (see `documentation/rulesets/`) enforce protections for `develop`, `release/*`, and tags.

## 8. How to extend

- Add new features as MediatR commands/queries plus validators and handlers in `Application`.
- Grow the domain by adding entities/interfaces in `Domain` and persistence in `Infrastructure`.
- Keep controllers thin; prefer application-level handlers for business rules.
- Add integration tests per layer to keep behaviors stable as the template evolves.
