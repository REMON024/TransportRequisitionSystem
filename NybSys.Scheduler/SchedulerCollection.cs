using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NybSys.Scheduler
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddScheduler(this IServiceCollection services)
        {
            services.AddSingleton<IHostedService, SchedulerService>();
            return services;
        }
    }
}

