using KitNugs.Services.Model;

namespace KitNugs.Services
{
    public class HelloService : IHelloService
    {
        private readonly IConfiguration configuration;

        public HelloService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<HelloModel> BusinessLogic(string name)
        {
            string t = configuration.GetValue<string>("TEST_VAR");
            return new HelloModel()
            {
                Name = name,
                DayOfWeek = DateTime.Now.DayOfWeek.ToString(),
                FromConfiguration = t,
            };
        }
    }
}
