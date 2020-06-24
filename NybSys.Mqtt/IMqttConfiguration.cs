using System;

namespace NybSys.Mqtt
{
    public interface IMqttConfiguration
    {
        void AddConnection(Action<MQTTConnector> connection);
        void AddConsumer<TConsumer>() where TConsumer : class, IMqttSubscribeTask;
    }
}
