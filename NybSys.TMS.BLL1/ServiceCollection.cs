using Microsoft.Extensions.DependencyInjection;

namespace NybSys.TMS.BLL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTMSBLL(this IServiceCollection services)
        {
            services.AddScoped<IRequisitionBLL, RequisitionServiceBLL>();
            services.AddScoped<IVehicleDocumentBLL, VehicleDocumentBLL>();
            services.AddScoped<ITravelDetailsBLL, TravelDetailsBLL>();
            
            return services;
        }
    }
}
