namespace KitNugs.Services
{
    public class HelloService : IHelloService
    {
        public HelloModel BusinessLogic()
        {
            return new HelloModel();
        }
    }
}
