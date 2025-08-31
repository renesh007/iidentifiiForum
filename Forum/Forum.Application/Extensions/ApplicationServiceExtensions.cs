using Forum.Application.Handlers;
using Forum.Application.Interfaces;
using Forum.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Forum.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        static public IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUserHandler, UserHandler>();
            services.TryAddScoped<IPostHandler, PostHandler>();
            services.TryAddScoped<ILikeHandler, LikeHandler>();
            services.TryAddScoped<ICommentHandler, CommentHandler>();
            services.TryAddScoped<ITagHandler, TagHandler>();
            services.TryAddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            return services;
        }
    }
}
