# Quick Setup Checklist (aspnet-ultimate-base)

Use this list after creating a new project from the template.

## 1) Install and verify
- [ ] Install .NET 10 SDK (`dotnet --info`)
- [ ] Restore packages: `dotnet restore AspNetUltimateBase.sln`
- [ ] Build & test: `dotnet build -c Release` and `dotnet test -c Release`

## 2) Git & repository
- [ ] Set default branch to `develop`
- [ ] Disable **Allow merge commits** and **Allow rebase merging**; keep squash merges enabled
- [ ] Enable **Automatically delete head branches** after merging pull requests
- [ ] Import rulesets from `documentation/rulesets/` (Develop, Release, Tag)
- [ ] Grant Actions **Read and write** permissions + allow Actions to create/approve PRs

## 3) Configuration
- [ ] Update `AspNetUltimateBase.Presentation/appsettings.json` or rely on environment overrides for `ConnectionStrings__AspNetUltimateBase`, `Jwt__Key`, `Jwt__Issuer`, `Jwt__ExpireInDays`
- [ ] Copy `nlog.config.example` to `nlog.config` and supply real targets/secrets
- [ ] Verify Swagger UI loads at `/` after running `dotnet run --project AspNetUltimateBase.Presentation`
- [ ] Configure CORS for your real origins in `AspNetUltimateBase.Presentation/Extensions/ServiceCollectionExtension.cs` (policy name lives in `AspNetUltimateBase.Presentation/ProgramConsts.cs` and is applied in `AspNetUltimateBase.Presentation/Program.cs`)

## 4) Docker
- [ ] Build image: `DOCKER_BUILDKIT=1 docker build -t your-org/aspnet-ultimate-base:local .`
- [ ] Run container with env vars for DB/JWT (see README for names)
- [ ] Confirm health via `/swagger` or `/api` endpoints

## 5) CI/CD
- [ ] Open a PR into `develop` and confirm CI runs restore/build/test
- [ ] Create the `Production` environment with required reviewers; add variables `DOCKERHUB_USERNAME`, `DOCKERHUB_REPOSITORY` and secret `DOCKERHUB_TOKEN` if publishing Docker images
- [ ] Trigger the release workflow with `version` and `version_name`; optionally enable Docker publishing
- [ ] (Optional) Run helper scripts locally if you want to preview/verify release changes:
  ```bash
  python .github/workflows/tools/update_changelog.py 9.9.9 "Test Release"
  python .github/workflows/tools/update_app_version.py 9.9.9
  python .github/workflows/tools/verify_release_readiness.py 9.9.9 "Test Release"
  ```
