using GreenPipes;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NybSys.MassTransit
{
    public interface IMassTransitRequest<T> where T : class
    {
        Task Send(T message, CancellationToken cancellationToken = default(CancellationToken));

        Task Send(T message, IPipe<SendContext<T>> pipe, CancellationToken cancellationToken = default(CancellationToken));

        Task Send(T message, IPipe<SendContext> pipe, CancellationToken cancellationToken = default(CancellationToken));

        Task Send(object message, CancellationToken cancellationToken = default(CancellationToken));

        Task Send(object message, Type messageType, CancellationToken cancellationToken = default(CancellationToken));

        Task Send(object message, IPipe<SendContext> pipe, CancellationToken cancellationToken = default(CancellationToken));

        Task Send(object message, Type messageType, IPipe<SendContext> pipe,
            CancellationToken cancellationToken = default(CancellationToken));

    }
}
