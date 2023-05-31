using KitNugs.Services.Model;

namespace KitNugs.Services
{
    public interface IHelloService
    {
        Task<HelloModel> BusinessLogic(string name);
    }
}
