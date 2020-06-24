using Microsoft.Extensions.DependencyInjection;

namespace NybSys.AuditLog.BLL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAuditLogBLL(this IServiceCollection services)
        {
            services.AddScoped<IAuditLogBLL, AuditLogBLL>();
            return services;
        }
    }
}
