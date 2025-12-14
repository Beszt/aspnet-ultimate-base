# AspNetUltimateBase
WEB API with info about food products (like energy, composition etc.) determined by EAN barcodes.

Live demo is available here [AspNetUltimateBase](http://AspNetUltimateBase.obisoft.pl).

## Technology stack

### Backend
- .NET 10.0
- ASP.NET Core Web API
- JWT authentication
- Roles authorization
- MediatR
- FluentValidation
- NLog
- Swagger

### Database 
- Entity Framework
- AutoMapper
- SQL Server

### Tests 
- Xunit
- FluentAssertion

### Patterns
- Clean Architecture
- CQRS

### DevOps
- CI/CD pipelines
- Branch protection rules
- Docker support

## Quick Setup
1. Install [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) and make new database.
2. Install [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0).
3. Download source code (all projects)
4. Navigate to `AspNetUltimateBase.Presentation` project folder and open `appsettings.json`.

    4.1. Set connection string to your SQL Server database instance. In example:
    ```
    "Data Source=localhost;Initial Catalog=FoodBarDb;User ID=sa;Password=YourStrong(!)Password;TrustServerCertificate=True;"
    ```
    4.2. Set JWT auhtentication config. In example:
    ```
    "Key": "YOUR VERY HIDDEN SERCET PHARSE",",
    "ExpireInDays": 14,
    "Issuer": "https://yourhost.com"
    ```

5. Change `nlog.config.example` to `nlog.config`. Optionally, if you want logging with email just replace smtp credentuals with yours and uncomment email logger.
6. Type `dotnet run`

## Quick Setup (Docker)
1. Install [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) and make new database.
2. Pull [Docker image](https://hub.docker.com/r/beszt/AspNetUltimateBase)
3. Run image with the following environment variables and change them values. In example:

```
ConnectionStrings__AspNetUltimateBase: Data Source=localhost;Initial Catalog=FoodBarDb;User ID=sa;Password=YourStrong(!)Password;TrustServerCertificate=True;
Jwt__Key: YOUR VERY HIDDEN SERCET PHARSE
Jwt__ExpireInDays: 14
Jwt__Issuer: https://yourhost.com
```

