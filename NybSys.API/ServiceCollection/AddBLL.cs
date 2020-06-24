using Microsoft.Extensions.DependencyInjection;
using NybSys.AuditLog.BLL;
using NybSys.Auth.BLL;
using NybSys.Generic.BLL;
using NybSys.Session.BLL;
using NybSys.TMS.BLL;

namespace NybSys.API.ServiceCollection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            services.AddAuthenticationBLL();
            services.AddSessionBLL();
            services.AddAuditLogBLL();
            services.AddGenericBLL();
            services.AddTMSBLL();

            return services;
        }
    }
}
