# Gym Progress — Backend (Phase 1: Authentication)

ASP.NET Core 9 Web API, Clean Architecture, built for the Gym Progress Flutter app.
This drop contains the **Authentication module** (Signup + Login) end to end.

## Solution layout

```
GymProgress/
└── src/
    ├── GymProgress.Shared          (no dependencies)
    ├── GymProgress.Domain          → Shared
    ├── GymProgress.Application     → Domain, Shared
    ├── GymProgress.Persistence     → Application, Domain   (EF Core / SQL Server)
    ├── GymProgress.Infrastructure  → Application, Domain   (JWT, BCrypt)
    └── GymProgress.API             → Application, Infrastructure, Persistence
```

Dependencies only ever point *inward*. Domain and Application know nothing about
EF Core, SQL Server, JWT, or ASP.NET Core — they only depend on interfaces they
themselves define. Persistence and Infrastructure implement those interfaces;
API wires everything together at startup. That's what makes this "Clean."

| Layer | Responsibility |
|---|---|
| **Shared** | `AppException` base type, `ApiResponse<T>` envelope — used everywhere, depends on nothing |
| **Domain** | `User`, `RefreshToken` entities; repository *contracts* (`IUserRepository`); domain exceptions |
| **Application** | Use-case logic (`AuthService`), DTOs, FluentValidation rules, AutoMapper profile, security *contracts* (`IPasswordHasher`, `ITokenGenerator`) |
| **Persistence** | `ApplicationDbContext`, EF Core entity configurations, repository *implementations*, SQL Server wiring |
| **Infrastructure** | JWT generation, BCrypt hashing — implementations of Application's security contracts |
| **API** | Controllers, middleware, `Program.cs`, configuration — the composition root |

### Where SOLID shows up

- **S**ingle Responsibility — `AuthController` only handles HTTP; `AuthService` only handles the register/login use case; `JwtTokenGenerator` only makes tokens.
- **O**pen/Closed — swap BCrypt for Argon2 by writing a new `IPasswordHasher` implementation; nothing else changes.
- **L**iskov Substitution — any `IUserRepository` implementation (SQL Server today, an in-memory fake in tests tomorrow) works wherever the interface is expected.
- **I**nterface Segregation — `IUserRepository` and `IRefreshTokenRepository` are separate, focused contracts instead of one bloated repository.
- **D**ependency Inversion — `AuthService` depends only on abstractions (`IUserRepository`, `IPasswordHasher`, `ITokenGenerator`), never on EF Core or BCrypt directly.

## Endpoints

| Method | Route | Body |
|---|---|---|
| POST | `/api/v1/auth/register` | `{ "fullName": "...", "email": "...", "password": "..." }` |
| POST | `/api/v1/auth/login` | `{ "email": "...", "password": "..." }` |

Both return:
```json
{
  "success": true,
  "message": "Login successful.",
  "data": {
    "userId": "...",
    "fullName": "...",
    "email": "...",
    "accessToken": "...",
    "accessTokenExpiresAtUtc": "...",
    "refreshToken": "..."
  },
  "errors": null
}
```
Validation or business-rule failures return the same envelope with `success: false` and a populated `errors` array.

## Setup (Windows / SSMS)

1. **Prerequisites**: .NET 10 SDK, SQL Server (the instance SSMS is pointed at), `dotnet-ef` tool.
   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. **Create the solution file** (this drop ships the projects, not a `.sln`, so the file matches whatever paths you unzip into):
   ```bash
   cd GymProgress
   dotnet new sln -n GymProgress
   dotnet sln add src/**/*.csproj
   dotnet restore
   ```

3. **Point it at your database.** Open `src/GymProgress.API/appsettings.json` and update `ConnectionStrings:DefaultConnection` to match your SSMS server name (e.g. `.\SQLEXPRESS`, `localhost`, or a named instance). If you use SQL authentication instead of Windows auth, use `User Id=...;Password=...;` instead of `Trusted_Connection=True;`.

4. **Set a real JWT secret.** Don't leave the placeholder in `appsettings.json` for anything beyond local testing — use `dotnet user-secrets` or an environment variable in real deployments:
   ```bash
   cd src/GymProgress.API
   dotnet user-secrets init
   dotnet user-secrets set "JwtSettings:Secret" "<a-long-random-string>"
   ```

5. **Create and apply the migration** (this generates the `Users` and `RefreshTokens` tables — open them in SSMS afterward to confirm):
   ```bash
   dotnet ef migrations add InitialCreate --project src/GymProgress.Persistence --startup-project src/GymProgress.API
   dotnet ef database update --project src/GymProgress.Persistence --startup-project src/GymProgress.API
   ```

6. **Run it:**
   ```bash
   dotnet run --project src/GymProgress.API
   ```
   Swagger UI opens at `https://localhost:<port>/swagger` in Development — use it to try Register/Login and to paste an access token into the "Authorize" button for any future protected endpoint.

## Notes for what's next (Phase 1 core feature)

- CORS is wide open (`AllowAnyOrigin`) to keep local Flutter testing friction-free — tighten this to your actual app's origin(s) before shipping.
- The daily workout-entry feature (Weight Training / Cardio / Rest) isn't in this drop — it'll be a new `WorkoutEntry` entity + its own controller, following the exact same pattern as Auth (repository interface in Domain, service in Application, EF configuration in Persistence).
- `[Authorize]` isn't yet applied to any endpoint since Auth is the only module so far — add it to future controllers to require a valid access token.
