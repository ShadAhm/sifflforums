using Microsoft.Extensions.DependencyInjection;
using SifflForums.Api.Services;
using SifflForums.Data;

namespace SifflForums.Api
{
    public static class StartupExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<SifflContext>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<ISubmissionsService, SubmissionsService>();
            services.AddScoped<IUsersService, UsersService>();
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
