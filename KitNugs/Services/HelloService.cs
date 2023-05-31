using KitNugs.Services.Model;

namespace KitNugs.Services
{
    public class HelloService : IHelloService
    {
        public async Task<HelloModel> BusinessLogic(string name)
        {
            return new HelloModel()
            {
                Name = name,
                DayOfWeek = DateTime.Now.DayOfWeek.ToString(),
            };
        }
    }
}
