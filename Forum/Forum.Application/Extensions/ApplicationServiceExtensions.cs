using Forum.Application.Handlers;
using Forum.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Forum.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        static public IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUserHandler, UserHandler>();
            return services;
        }
    }
}
