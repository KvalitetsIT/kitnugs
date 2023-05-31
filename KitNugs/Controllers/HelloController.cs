using KitNugs.Services;
using Microsoft.AspNetCore.Mvc;

namespace KitNugs.Controllers
{
    public class HelloController : HelloControllerBase
    {
        private readonly ILogger<HelloController> _logger;
        private readonly IHelloService helloService;

        public HelloController(ILogger<HelloController> logger, IHelloService helloService)
        {
            _logger = logger;
            this.helloService = helloService;
        }

        public override async Task<HelloResponse> Hello([FromQuery] string name)
        {
            var businessResult = await helloService.BusinessLogic(name);

            return new HelloResponse { 
                Now = businessResult.Now, 
                Name = businessResult.Name,
                From_configuration = businessResult.FromConfiguration
            };
        }
    }
}
