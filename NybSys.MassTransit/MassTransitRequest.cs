using GreenPipes;
using MassTransit;
using NybSys.MassTransit.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NybSys.MassTransit
{
    public class MassTransitRequest<T> : IMassTransitRequest<T> where T : class
    {
        private readonly ISendEndpoint _sendEndpoint;

        public MassTransitRequest(IBusControl busControl, RabbitMQConnector connector, string queueName)
        {
            _sendEndpoint = busControl.GetSendEndpoint(GetEndpointAddress(connector, queueName)).GetAwaiter().GetResult();
        }

        public async Task Send(T message, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _sendEndpoint.Send<T>(message, cancellationToken);
        }

        public async Task Send(T message, IPipe<SendContext<T>> pipe, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _sendEndpoint.Send<T>(message,pipe, cancellationToken);
        }

        public async Task Send(T message, IPipe<SendContext> pipe, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _sendEndpoint.Send(message,pipe, cancellationToken);
        }

        public async Task Send(object message, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _sendEndpoint.Send(message, cancellationToken);
        }

        public async Task Send(object message, Type messageType, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _sendEndpoint.Send(message, messageType, cancellationToken);
        }

        public async Task Send(object message, IPipe<SendContext> pipe, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _sendEndpoint.Send(message, pipe, cancellationToken);
        }

        public async Task Send(object message, Type messageType, IPipe<SendContext> pipe, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _sendEndpoint.Send(message, messageType, pipe, cancellationToken);
        }

        private Uri GetEndpointAddress(RabbitMQConnector rabbitMQConnector, string queueName)
        {
            return new Uri(RabbitMqUriBuilder.RabbitMQConnectionStringBuilder(rabbitMQConnector.RabbitMQConnectionString, queueName));
        }
    }
}
