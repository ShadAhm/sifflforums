# Siffl Forums
Short for "Simple Forums For Learning". Siffl is a forum/bulletin board engine designed for simplicity and also speed. The primary design inspiration is [Reddit](https://www.reddit.com/"), but we are also trying to make it it's own thing.  

## Getting started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js](https://nodejs.org/) 20.19+, 22.12+ or 24+
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) using Linux containers
- VS Code with the C# Dev Kit extension

### First-time setup

1. Copy `.env.example` to `.env` and set `MSSQL_SA_PASSWORD` to a strong local password.
2. Store the matching SQL Server connection string in .NET user secrets:

	```powershell
	dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,14330;Database=Siffl;User Id=sa;Password=<your-password>;TrustServerCertificate=True" --project SifflForums.Api
	```

3. Install the EF Core CLI if needed, then apply the migrations once:

	```powershell
	dotnet tool install --global dotnet-ef
	$env:ASPNETCORE_ENVIRONMENT = "Development"
	dotnet ef database update -p SifflForums.Data -s SifflForums.Api
	```

Or use **Tasks: Run Task** > **Apply Database Migrations** in VS Code; that task starts the database and sets the Development environment before applying the schema. The API does not automatically apply migrations.

### Run everything in VS Code

1. Start Docker Desktop.
2. Open **Run and Debug**, select **Run All**, and press **F5**.

VS Code starts the SQL Server container, the API, and the Web project. The Web project starts Angular through the SPA proxy. The app opens at `https://localhost:44301`; the API is at `https://localhost:44302`.

To apply later migrations, run **Tasks: Run Task** and select **Apply Database Migrations**. To stop the database while preserving its data, run `docker compose down` from the repository root. To delete the database and its data, run `docker compose down -v`.

### Manual run
- **API** (`https://localhost:44302`): `dotnet run --project SifflForums.Api`
- **Web** (Angular SPA): `dotnet run --project SifflForums.Web` starts `ng serve` via the SPA proxy, or run `npm start` in `SifflForums.Web/ClientApp` and browse to `http://localhost:4200`.

Authentication uses the ASP.NET Core Identity API endpoints (`/api/auth/register`, `/api/auth/login`, `/api/auth/refresh`, ...) with bearer tokens.
