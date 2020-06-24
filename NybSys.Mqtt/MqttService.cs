using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NybSys.Mqtt
{
    public class MqttService : IMqttService
    {
        private readonly IMqttClientOptions _options;
        private readonly IEnumerable<IMqttSubscribeTask> _subscribeTasks;
        private IMqttClient _mqttClient;

        public MqttService(
            IMqttClientOptions options,
            IEnumerable<IMqttSubscribeTask> subscribeTasks)
        {
            _options = options;
            _subscribeTasks = subscribeTasks;
            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            _mqttClient.Connected += MqttClient_Connected;
            _mqttClient.Disconnected += MqttClient_DisConnected;
            _mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
        }


        public async Task ConnectAsync()
        {
            await MqttClient_Connect();
            await MultipleSubscribeAsync();

        }

        public async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync();
        }

        public async Task PublishMessage(string topic, object payloadMessage)
        {
            if (!_mqttClient.IsConnected)
            {
                await MqttClient_Connect();
            }


            var mqqtMessage = new MqttApplicationMessageBuilder()
                        .WithTopic(topic)
                        .WithPayload(JsonConvert.SerializeObject(payloadMessage))
                        .WithAtLeastOnceQoS()
                        .Build();


            await PublishMessage(mqqtMessage);
        }

        public async Task SubscribeAsync(string topic)
        {
            await SubscribedAsync(topic);
        }

        private async void MqttClient_DisConnected(object s, MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine("### DISCONNECTED FROM SERVER ###");
            await Task.Delay(TimeSpan.FromSeconds(5));

            await MqttClient_Connect();
        }

        private async void MqttClient_Connected(object s, MqttClientConnectedEventArgs e)
        {
            Console.WriteLine("### CONNECTED WITH SERVER ###");
            await Task.CompletedTask;
        }

        private async void MqttClient_ApplicationMessageReceived(object s, MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                IMqttSubscribeTask mqttSubscribeTask = _subscribeTasks.Where(subscribe => subscribe.Topic == e.ApplicationMessage.Topic).FirstOrDefault();
                if (mqttSubscribeTask != null)
                {
                    string payloadMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    await mqttSubscribeTask.MessageReceivedAsync(payloadMessage);
                }
                else
                {
                    await Task.CompletedTask;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("##Exceptopn at {0} : {1}", DateTime.Now, JsonConvert.SerializeObject(ex)));
            }
        }

        private async Task MultipleSubscribeAsync()
        {
            foreach(var subscribeTask in _subscribeTasks)
            {
                await SubscribeAsync(subscribeTask.Topic);
            }
        }

        private async Task SubscribedAsync(string topic)
        {
            IList<MqttSubscribeResult> res = await _mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).WithAtLeastOnceQoS().Build());
        }

        private async Task MqttClient_Connect()
        {
            //try
            //{
            //    await _mqttClient.ConnectAsync(_options);
            //    Console.WriteLine("### CONNECTION IS STABLISHED ###");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("### RECONNECTING FAILED ###");
            //    Thread.Sleep(TimeSpan.FromSeconds(20));
            //    await MqttClient_Connect();
            //    await Task.CompletedTask;
            //}

            await _mqttClient.ConnectAsync(_options);
        }

        private async Task PublishMessage(MqttApplicationMessage mqttMessage)
        {
            try
            {
                await _mqttClient.PublishAsync(mqttMessage);
            }
            catch (MqttCommunicationTimedOutException)
            {
                if (_mqttClient.IsConnected)
                {
                    await PublishMessage(mqttMessage);
                }
                await Task.CompletedTask;
            }
            catch (Exception)
            {
                if (_mqttClient.IsConnected)
                {
                    await PublishMessage(mqttMessage);
                }

                await Task.CompletedTask;
            }
        }
    }

    public interface IMqttService
    {
        Task PublishMessage(string topic, object payloadMessage);
        Task SubscribeAsync(string topic);
        Task DisconnectAsync();
        Task ConnectAsync();
    }
}
