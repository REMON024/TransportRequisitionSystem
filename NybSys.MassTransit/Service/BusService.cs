using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NybSys.MassTransit.Service
{
    public class BusService : IHostedService
    {
        private readonly IBusControl _busControl;

        public BusService(IServiceProvider serviceProvider)
        {
            _busControl = serviceProvider.GetService<IBusControl>();
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Start......");
            await _busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Closing....");
            await _busControl.StopAsync(cancellationToken);
        }
    }
}
