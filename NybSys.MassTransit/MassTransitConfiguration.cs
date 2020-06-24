using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.DependencyInjection;
using NybSys.MassTransit.Service;
using NybSys.MassTransit.Utility;
using System;

namespace NybSys.MassTransit
{
    public class MassTransitConfiguration : IMassTransitConfiguration
    {
        private readonly IServiceCollection _services;


        public MassTransitConfiguration(IServiceCollection services)
        {
            _services = services;
        }

        public void AddRequestMessage<TRequest>(string queueName) where TRequest : class
        {
            _services.AddSingleton<IMassTransitRequest<TRequest>>(provider =>
            {
                RabbitMQConnector connector = provider.GetService<RabbitMQConnector>();
                IBusControl bsuControl = provider.GetRequiredService<IBusControl>();
                return new MassTransitRequest<TRequest>(bsuControl, connector, queueName);
            });
        }

        public void AddRequestResponse<TRequest, TResponse>(string queueName)
            where TRequest : class
            where TResponse : class
        {
            _services.AddScoped<IRequestClient<TRequest, TResponse>>(provider =>
            {
                RabbitMQConnector massTransitconnector = provider.GetService<RabbitMQConnector>();
                var serviceAddress = new Uri(RabbitMqUriBuilder.RabbitMQConnectionStringBuilder(massTransitconnector.RabbitMQConnectionString, queueName));

                IBus bus = provider.GetRequiredService<IBus>();

                return new MessageRequestClient<TRequest, TResponse>(bus, serviceAddress, TimeSpan.FromSeconds(massTransitconnector.Timeout), TimeSpan.FromSeconds(2000));
            });
            
        }

        public void SetupConnection(Action<RabbitMQConnector> connection , string SubscribeQueueName = null)
        {
            RabbitMQConnector connector = new RabbitMQConnector();
            connection?.Invoke(connector);
            _services.AddSingleton(connector);

            if (string.IsNullOrEmpty(SubscribeQueueName))
            {
                AddClientMassTransit();

            }else
            {
                CompleteRabbitMqConnection completeConnection = new CompleteRabbitMqConnection();

                connection?.Invoke(completeConnection);
                completeConnection.SubscribeTopics = SubscribeQueueName;

                _services.AddSingleton(completeConnection);

                AddMassTransitWithRabbitMq();
            }
        }

        void IMassTransitConfiguration.AddCosumers(Action<IServiceCollectionConfigurator> configure)
        {
            _services.AddMassTransit(configure);
        }

        private void AddClientMassTransit()
        {
            _services.AddSingleton<IBusControl>(context =>
            {
                RabbitMQConnector connector = context.GetService<RabbitMQConnector>();

                var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri(RabbitMqUriBuilder.RabbitMQConnectionStringBuilder(connector.RabbitMQConnectionString)), h =>
                    {
                        h.Username(connector.Username);
                        h.Password(connector.Password);
                    });
                });
                return bus;
            });

            _services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            _services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            _services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());

            _services.AddHostedService<BusService>();
        }

        private void AddMassTransitWithRabbitMq()
        {
            _services.AddSingleton<IBusControl>(context =>
            {

                CompleteRabbitMqConnection connector = context.GetService<CompleteRabbitMqConnection>();

                var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri(RabbitMqUriBuilder.RabbitMQConnectionStringBuilder(connector.RabbitMQConnectionString)), h =>
                    {
                        h.Username(connector.Username);
                        h.Password(connector.Password);
                    });

                    cfg.ReceiveEndpoint(host, connector.SubscribeTopics, e =>
                    {
                        e.LoadFrom(context);
                    });
                });

                return bus;
            });
            _services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            _services.AddHostedService<BusService>();
        }
    }
}
