using KitNugs.Services;
using Microsoft.AspNetCore.Mvc;

namespace KitNugs.Controllers
{
    public class HelloController : AbstractHelloController
    {
        

        private readonly ILogger<HelloController> _logger;
        private readonly IHelloService helloService;

        public HelloController(ILogger<HelloController> logger, IHelloService helloService)
        {
            _logger = logger;
            this.helloService = helloService;
        }

        public override HelloResponse Get(String name)
        {
            var businessResult = helloService.BusinessLogic(name);
            return new HelloResponse { ToDay = businessResult.DayOfWeek, Name = businessResult.Name };
        }
    }

    public class HelloResponse
    {
        public required String ToDay { get; set; }
        public required String Name { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    abstract public class AbstractHelloController : ControllerBase
    {
        [HttpGet(Name = "Hello")]
        abstract public HelloResponse Get([FromQuery] String name);
    }
}

