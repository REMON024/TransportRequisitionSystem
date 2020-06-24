using System.Threading;
using System.Threading.Tasks;

namespace NybSys.Mqtt
{
    public interface IMqttSubscribeTask
    {
        string Topic { get; }
        Task MessageReceivedAsync(string message);
    }
}
