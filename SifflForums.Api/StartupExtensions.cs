using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SifflForums.Api.Models.Auth;
using SifflForums.Api.Services;
using SifflForums.Api.Services.Validators;
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
            services.AddScoped<IUpvotesService, UpvotesService>();
        }

        public static void AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddTransient<IValidator<SignUpViewModel>, SignUpValidator>(); 
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
