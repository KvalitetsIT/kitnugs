using KitNugs.Services;
using Microsoft.AspNetCore.Mvc;
using Org.OpenAPITools.Controllers;
using Org.OpenAPITools.Models;

namespace KitNugs.Controllers
{
    public class HelloController :  KITHUGSApiController
    {
        private readonly ILogger<HelloController> _logger;
        private readonly IHelloService _helloService;

        public HelloController(ILogger<HelloController> logger, IHelloService helloService)
        {
            _logger = logger;
            _helloService = helloService;
        }

        public override async Task<IActionResult> V1HelloGet(string name)
        {
            _logger.LogInformation("Entering GET!");
            var businessResult = await _helloService.BusinessLogic(name);

            var response = new HelloResponse()
            {
                Now = businessResult.Now.DateTime,
                Name = businessResult.Name,
                From_configuration = businessResult.FromConfiguration
            };

            return Ok(response);
        }
    }
}
