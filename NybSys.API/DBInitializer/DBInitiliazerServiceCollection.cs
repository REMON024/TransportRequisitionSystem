using Microsoft.Extensions.DependencyInjection;

namespace NybSys.API.DBInitializer
{
    public static class DBInitiliazerServiceCollection
    {
        public static IServiceCollection AddDBInitializer(this IServiceCollection services)
        {
            services.AddScoped<IDBInitializer, DBInitializer>();
            return services;
        }
    }
}
