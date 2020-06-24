/* 
 Created By : Md. Nahid Hasan
 Created Date : 27-11-2018
 */
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NybSys.Mqtt
{
    public static class ServiceCollectionExtension
    {       
        public static IServiceCollection AddMqttServer(this IServiceCollection services, Action<IMqttConfiguration> config)
        {
            var configuration = new MqttConfiguration(services);
            config?.Invoke(configuration);

            services.AddSingleton(configuration);

            return services;
        }
        

    }
}