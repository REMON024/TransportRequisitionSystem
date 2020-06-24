namespace NybSys.MassTransit.Utility
{
    public static class RabbitMqUriBuilder
    {
        public static string RabbitMQConnectionStringBuilder(string uri)
        {
            return string.Format("rabbitmq://{0}/", uri);
        }

        public static string RabbitMQConnectionStringBuilder(string uri, string queueName)
        {
            return string.Format("rabbitmq://{0}/{1}", uri, queueName);
        }
    }
}
