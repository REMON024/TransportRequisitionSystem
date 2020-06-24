using MassTransit;
using NybSys.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace NybSys.Worker.Request_Handler
{
    public class RequestConsumer : IConsumer<Models.ViewModels.Request>
    {
        public async Task Consume(ConsumeContext<Request> context)
        {
            Console.WriteLine(context.Message.RequestMessage);

            await context.RespondAsync<Models.ViewModels.Response>(new Response()
            {
                ResponseMessage = "This is response message from worker"
            });
        }
    }
}
