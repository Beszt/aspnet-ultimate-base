<p align="center">
  <img src="https://img.shields.io/github/actions/workflow/status/Beszt/aspnet-ultimate-base/ci.yml?label=CI%20Build&logo=githubactions&logoColor=white&color=22c55e" alt="CI status">
  <img src="https://img.shields.io/github/actions/workflow/status/Beszt/aspnet-ultimate-base/release.yml?label=Release&logo=semanticrelease&logoColor=white&color=0ea5e9" alt="Release status">
  <img src="https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/Clean%20Architecture-CQRS-64748b" alt="Clean Architecture">
  <img src="https://img.shields.io/github/license/Beszt/aspnet-ultimate-base?color=f97316&logo=open-source-initiative&logoColor=white" alt="License">
</p>

# aspnet-ultimate-base

Template .NET 10 Web API for a ready-to-code backend starter. Clean Architecture + CQRS + MediatR + FluentValidation + EF Core + JWT + NLog, with Docker and GitHub Actions ready out of the box.

## Highlights
- Clean Architecture layers (Domain/Application/Infrastructure/Presentation) with MediatR, FluentValidation, and AutoMapper
- JWT authentication with role-based authorization and Swagger UI ready for interactive testing
- EF Core + SQL Server with repository layer, migrations, and seeders for demo data
- NLog logging configuration (`nlog.config.example` provided)
- GitHub Actions for PR CI (develop) and a release workflow that bumps changelog + version, builds/tests/tags, and optionally pushes Docker images
- Repository rulesets and docs included so new clones share the same conventions

## Getting started
1. Install [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) and ensure `dotnet --info` works.
2. Restore packages: `dotnet restore AspNetUltimateBase.sln`.
3. Configure `AspNetUltimateBase.Presentation/appsettings.json` (or environment variables listed below) with your SQL connection string and JWT secrets. Copy `nlog.config.example` to `nlog.config` if you want file/email logging.
4. Run locally: `dotnet run --project AspNetUltimateBase.Presentation -c Release` and open Swagger at the URL printed in logs (`http://localhost:5164` or `https://localhost:7149` by default).
5. Walk through `documentation/CHECKLIST.md` and `documentation/ARCHITECTURE.md` to align tooling and conventions.

## Runtime configuration

Settings can be provided via `appsettings*.json` or overridden by environment variables using double underscores:

| Environment variable | Default (appsettings.json) | Purpose |
| -------------------- | -------------------------- | ------- |
| `ConnectionStrings__AspNetUltimateBase` | `"PUT HERE YOUR CONNECTION STRING"` | SQL Server connection string |
| `Jwt__Key` | `"YOUR VERY LONG HIDDEN SECRET HERE"` | Symmetric signing key |
| `Jwt__Issuer` | `"YOUR URL HERE"` | Token issuer / audience |
| `Jwt__ExpireInDays` | `14` | Token lifetime in days |

`ASPNETCORE_ENVIRONMENT` controls which `appsettings.<Environment>.json` file is loaded; `Development` uses `appsettings.Development.json`.

## Docker

Build once and configure per environment with the same env vars used by ASP.NET configuration:

```bash
DOCKER_BUILDKIT=1 docker build -t your-org/aspnet-ultimate-base:local .

docker run --rm -p 8080:8080 \
  -e ConnectionStrings__AspNetUltimateBase="Server=sql:1433;Database=aspnetultimatebase;User Id=sa;Password=Strong(!)Password;TrustServerCertificate=True;" \
  -e Jwt__Key="super-secret-key" \
  -e Jwt__Issuer="https://your-host" \
  your-org/aspnet-ultimate-base:local
```

## GitHub Actions & releases

- CI: runs on pull requests to `develop` (`.github/workflows/ci.yml`) and executes restore, build, and tests.
- Release: manual dispatch (`.github/workflows/release.yml`) that creates `release/X.Y.Z`, updates `documentation/CHANGELOG.md` and `AspNetUltimateBase.Presentation/ProgramInfo.cs`, builds/tests/publishes the API, tags `vX.Y.Z`, attaches a ZIP artifact, and optionally pushes Docker images to `DOCKERHUB_REPOSITORY` when credentials are provided in the `Production` environment.
- Default branch: `develop`; import rulesets from `documentation/rulesets/` and follow `documentation/GITHUB_SETTINGS.md` for repository settings.

## Additional documentation

- `documentation/ARCHITECTURE.md` - layer breakdown and conventions
- `documentation/CHECKLIST.md` - post-clone tasks
- `documentation/GITHUB_SETTINGS.md` - recommended GitHub settings and rulesets
- `documentation/CHANGELOG.md` - release notes maintained automatically by the workflow
- `.github/workflows/` - CI and release definitions
