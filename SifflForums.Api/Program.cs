using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SifflForums.Api;
using SifflForums.Data;
using SifflForums.Data.Entities;

const string devCorsPolicy = "_devCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SifflContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(IdentityConstants.BearerScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorization();

// No email sender is configured, so accounts don't require confirmation
builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        // Matches the existing schema created by the previous ApiAuthorization/IdentityServer setup
        options.Stores.MaxLengthForKeys = 128;
    })
    .AddEntityFrameworkStores<SifflContext>()
    .AddApiEndpoints();

builder.Services.AddCors(options =>
{
    // The SPA authenticates with bearer tokens (not cookies), so any origin is safe in development
    options.AddPolicy(devCorsPolicy, policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddDataAccessServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors(devCorsPolicy);
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("/api/auth").MapIdentityApi<ApplicationUser>();

app.Run();
