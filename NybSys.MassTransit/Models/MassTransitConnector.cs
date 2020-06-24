namespace NybSys.MassTransit
{
    public class RabbitMQConnector
    {
        public string RabbitMQConnectionString { get; set; } = "localhost";
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string Port { get; set; } = "";
        public int Timeout { get; set; } = 60;
    }

    public class CompleteRabbitMqConnection : RabbitMQConnector
    {
        public string SubscribeTopics { get; set; }
    }
}
