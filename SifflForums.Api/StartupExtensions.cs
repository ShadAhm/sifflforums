using Microsoft.Extensions.DependencyInjection;
using SifflForums.Data;
using SifflForums.Service;
using SifflForums.Data.Services;

namespace SifflForums.Api
{
    public static class StartupExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<SifflContext>();
            services.AddTransient<IUserResolverService, UserResolverService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<ISubmissionsService, SubmissionsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUpvotesService, UpvotesService>();
            services.AddScoped<IForumSectionsService, ForumSectionsService>();
        }
    }
}
