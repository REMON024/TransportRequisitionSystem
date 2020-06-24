using MassTransit.ExtensionsDependencyInjectionIntegration;
using System;

namespace NybSys.MassTransit
{
    public interface IMassTransitConfiguration
    {
        void SetupConnection(Action<RabbitMQConnector> connection , string queueName = null);
        void AddCosumers(Action<IServiceCollectionConfigurator> configure = null);
        void AddRequestMessage<T>(string queueName) where T : class;
        void AddRequestResponse<TRequest, TResponse>(string queueName) where TRequest : class where TResponse : class;
    }
}
