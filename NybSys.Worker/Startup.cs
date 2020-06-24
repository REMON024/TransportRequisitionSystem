using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NybSys.Common.Utility;
using System;
using System.IO;
using NybSys.MassTransit;
using NybSys.Mqtt;
using NybSys.Worker.Request_Handler;

namespace NybSys.Worker
{
    class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /// Dependency Injection Implementation Here Or Inject Servie Collection
            /// services.AddTransit<>();
            /// Here Input the BaseUri of HttpRequest


            services.AddAspNetCoreMassTransit(config =>
            {
                config.SetupConnection(connection =>
                {
                    connection.RabbitMQConnectionString = "10.200.10.222";
                    connection.Port = "5672";
                    connection.Timeout = 60;
                    connection.Username = "admin";
                    connection.Password = "a";
                }, "Worker");

                config.AddCosumers(consumer =>
                {
                    consumer.AddConsumer<RequestConsumer>();
                });
            });

            services.AddMqttServer(config =>
            {
                config.AddConnection(connection =>
                {
                    connection.MQBrokerHost = "10.200.10.222";
                    connection.MQbrokerPort = 1883;
                    connection.AlivePeriod = 60;
                    connection.ConnectionTimeout = 30;
                });

                config.AddConsumer<MqttConsumer>();
            });


            // This is service provider for create instance of DI object
            ServiceProvider provider = services.BuildServiceProvider();

            // This is an example of how to create instance of DI object

            EncryptionService encryptionService = new EncryptionService();

            Console.WriteLine(encryptionService.Encrypt("a"));




            // Now you will able to execute your desired process

        }
    }
}
