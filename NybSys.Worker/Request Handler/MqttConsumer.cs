using System;
using System.Threading.Tasks;
using NybSys.Mqtt;

namespace NybSys.Worker.Request_Handler
{
    public class MqttConsumer : IMqttSubscribeTask
    {
        public string Topic => "NewWorker";

        public async Task MessageReceivedAsync(string message)
        {
            Console.WriteLine(message);

            await Task.CompletedTask;
        }
    }
}
