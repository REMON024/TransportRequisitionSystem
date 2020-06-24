using Microsoft.Extensions.DependencyInjection;
using NybSys.API.Manager;

namespace NybSys.API.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddManager(this IServiceCollection services)
        {
            services.AddScoped<ISecurityManager, SecurityManager>();
            services.AddScoped<IUserManager, SecurityManager>();
            services.AddScoped<IAccessManager, SecurityManager>();
            services.AddScoped<ILogManager, LogManager>();
            services.AddScoped<IRequisitionManager, RequisitionManager>();
            services.AddScoped<IVehicleTypeManager, VehicleManager>();

            //services.AddScoped<IVehicleDocumentManager, VehicleManager>();

            services.AddScoped<IMailManager, MailManager>();

            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<IDriverManager, DriverManager>();
            services.AddScoped<ITravelDetailsManager, TravelDetailManager>();
            services.AddScoped<IAdvancedReportSearchManager, AdvancedReportSearchManager>();

            return services;
        }
    }
}
