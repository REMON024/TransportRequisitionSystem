using System.Threading;
using System.Threading.Tasks;

namespace NybSys.Mqtt
{
    public class MqttSubscribeService : HostedService
    {
        private readonly IMqttService _mqttService;

        public MqttSubscribeService(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _mqttService.ConnectAsync();
        }
    }
}
