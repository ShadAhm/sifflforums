# Siffl Forums
Short for "Simple Forums For Learning". Siffl is a forum/bulletin board engine designed for simplicity and also speed. The primary design inspiration is [Reddit](https://www.reddit.com/"), but we are also trying to make it it's own thing.  

## Getting started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js](https://nodejs.org/) 20.19+, 22.12+ or 24+
- SQL Server (the connection string lives in `SifflForums.Api/appsettings.json`)

### Database
```
dotnet tool install --global dotnet-ef
dotnet ef database update -p SifflForums.Data -s SifflForums.Api
```

### Running
- **API** (`https://localhost:44302`): `dotnet run --project SifflForums.Api`
- **Web** (Angular SPA): `dotnet run --project SifflForums.Web` starts `ng serve` via the SPA proxy, or run `npm start` in `SifflForums.Web/ClientApp` and browse to `http://localhost:4200`.

Authentication uses the ASP.NET Core Identity API endpoints (`/api/auth/register`, `/api/auth/login`, `/api/auth/refresh`, ...) with bearer tokens.
