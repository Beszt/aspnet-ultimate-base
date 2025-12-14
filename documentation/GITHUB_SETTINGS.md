# GitHub settings

Apply these repository settings after creating a project from this template so CI/CD and releases work smoothly.

## Pull requests and merges
- Set the default branch to `develop`
- Disable **Allow merge commits** and **Allow rebase merging**; keep squash merges enabled
- Enable **Automatically delete head branches** after merging pull requests

## Repository rulesets (exports included)
- Import JSON exports under `documentation/rulesets/` via **Settings -> Rules -> Rulesets -> Import** and upload each file (`Develop.json`, `Release.json`, `Tag.json`)
- Summaries:
  - **Develop** (`refs/heads/develop`): blocks deletions/force pushes, enforces linear history, allows only squash merges, requires 1 approving review with resolved threads
  - **Release** (`refs/heads/release/*`): blocks deletions/force pushes and enforces linear history
  - **Tag** (all tags): blocks tag deletions and force updates
- Adjust reviewer counts/bypass actors to fit your team if needed

## Actions workflow permissions
- In **Settings -> Actions -> General**, set Workflow permissions to **Read and write permissions**
- Enable **Allow GitHub Actions to create and approve pull requests**

## Production environment
- Create an environment named `Production` with required reviewers enabled
- Add environment variables `DOCKERHUB_USERNAME` and `DOCKERHUB_REPOSITORY` (values for your Docker Hub account)
- Add the environment secret `DOCKERHUB_TOKEN` for authentication
- The `release.yml` workflow reads these when Docker publishing is enabled
