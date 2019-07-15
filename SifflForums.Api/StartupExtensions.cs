using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SifflForums.AutoMapperProfiles;
using SifflForums.Data;
using SifflForums.Service;

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
            services.AddScoped<IUpvotesService, UpvotesService>();
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            var config = new MapperConfiguration(c => {
                c.AddProfile<CommentsProfile>();
                c.AddProfile<SubmissionsProfile>();
                c.AddProfile<UsersProfile>();
            });

            services.AddSingleton<IMapper>(s => config.CreateMapper());
        }
    }
}
