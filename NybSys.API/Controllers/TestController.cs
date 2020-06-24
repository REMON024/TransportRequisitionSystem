using MassTransit;
using Microsoft.AspNetCore.Mvc;
using NybSys.Mqtt;
using System.Threading.Tasks;

namespace NybSys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRequestClient<Models.ViewModels.Request, Models.ViewModels.Response> _requestClient;
        private readonly IMqttService _mqttServicel;

        public TestController(
                IRequestClient<Models.ViewModels.Request, Models.ViewModels.Response> requestClient,
                IMqttService mqttServicel
            )
        {
            _requestClient = requestClient;
            _mqttServicel = mqttServicel;
        }

        [HttpPost("test-masstransit")]
        public async Task<IActionResult> TestMassTransit()
        {
            var response = await _requestClient.Request(new Models.ViewModels.Request()
                                                        {
                                                            RequestMessage = "This message from api server"
                                                        });

            return Ok(response.ResponseMessage);
        }

        [HttpPost("test-mqtt")]
        public async Task<IActionResult> TestRabbitMq()
        {
            await _mqttServicel.PublishMessage("NewWorker", "This is message from api server (Mqtt)");

            return Ok("Message Sent Successfully");
        }
    }
}