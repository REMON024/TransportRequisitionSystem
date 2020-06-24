using Microsoft.Extensions.DependencyInjection;

namespace NybSys.Session.BLL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSessionBLL(this IServiceCollection services)
        {
            services.AddScoped<ISessionBLL, SessionBLL>();
            services.AddScoped<IRedisSessionBLL, SessionBLL>();

            return services;
        }
    }
}
