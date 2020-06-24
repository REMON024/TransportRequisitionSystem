using Microsoft.Extensions.DependencyInjection;

namespace NybSys.Generic.BLL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddGenericBLL(this IServiceCollection services)
        {
            services.AddScoped<IGenericBLL, GenericBLL>();
            return services;
        }
    }
}
