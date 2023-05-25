using KitNugs.Services;
using Microsoft.AspNetCore.Mvc;

namespace KitNugs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        

        private readonly ILogger<HelloController> _logger;
        private readonly IHelloService helloService;

        public HelloController(ILogger<HelloController> logger, IHelloService helloService)
        {
            _logger = logger;
            this.helloService = helloService;
        }

        [HttpGet(Name = "Hello")]
        public HelloResponse Get([FromQuery] String name)
        {
            var businessResult = helloService.BusinessLogic(name);
            return new HelloResponse { ToDay = businessResult.DayOfWeek, Name = businessResult.Name };
        }
    }

    public class HelloResponse
    {
        public String ToDay { get; set; }
        public String Name { get; set; }
    }
}