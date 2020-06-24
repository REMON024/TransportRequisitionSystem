using Microsoft.Extensions.DependencyInjection;
using System;

namespace NybSys.MassTransit
{
    public static class MassTransitServiceCollection
    {
        public static IServiceCollection AddAspNetCoreMassTransit(this IServiceCollection services , Action<IMassTransitConfiguration> config)
        {
            var configurator = new MassTransitConfiguration(services);

            config?.Invoke(configurator);

            services.AddSingleton(configurator);

            return services; ;
        }
    }
}
