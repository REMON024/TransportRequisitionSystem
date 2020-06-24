using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.Client;
using System;

namespace NybSys.Mqtt
{
    public class MqttConfiguration: IMqttConfiguration
    {
        private readonly IServiceCollection _services;

        public MqttConfiguration(IServiceCollection services)
        {
            _services = services;
        }

        public void AddConnection(Action<MQTTConnector> connection)
        {
            MQTTConnector connector = new MQTTConnector();
            connection?.Invoke(connector);

            _services.AddSingleton(provider =>
            {
                return GetClientOptions(connector);
            });

            AddMqtt(_services);
        }

        public void AddConsumer<TConsumer>() where TConsumer : class,IMqttSubscribeTask
        {
            _services.AddSingleton<IMqttSubscribeTask, TConsumer>();
        }

        private static IMqttClientOptions GetClientOptions(MQTTConnector connector)
        {
            MqttClientOptionsBuilder clientOptionsBuilder = new MqttClientOptionsBuilder();

            clientOptionsBuilder = clientOptionsBuilder.WithClientId(string.IsNullOrEmpty(connector.ClientId) ? Guid.NewGuid().ToString() : connector.ClientId)
                                                       .WithCleanSession()
                                                       .WithKeepAlivePeriod(connector.AlivePeriod == 0 ? TimeSpan.FromSeconds(60) : TimeSpan.FromSeconds(connector.AlivePeriod))
                                                       .WithCommunicationTimeout(connector.ConnectionTimeout == 0 ? TimeSpan.FromSeconds(30) : TimeSpan.FromSeconds(connector.ConnectionTimeout))
                                                       .WithTcpServer(
                                                            string.IsNullOrEmpty(connector.MQBrokerHost) ? "localhost" : connector.MQBrokerHost,
                                                            connector.MQbrokerPort == 0 ? 1883 : connector.MQbrokerPort
                                                        );

            if (string.IsNullOrEmpty(connector.MQUserID) || string.IsNullOrEmpty(connector.MQPassword))
            {
                return clientOptionsBuilder.Build();
            }
            else
            {
                clientOptionsBuilder = clientOptionsBuilder.WithCredentials(connector.MQUserID, connector.MQPassword);
                return clientOptionsBuilder.Build();
            }
        }

        private static IServiceCollection AddMqtt(IServiceCollection services)
        {
            services.AddSingleton<IMqttService, MqttService>();
            services.AddSingleton<IHostedService, MqttSubscribeService>();

            return services;
        }
    }
}
