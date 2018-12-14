using System.Threading.Tasks;

namespace Pugster.Bridge
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var startup = new Startup(args);
            await startup.RunAsync();
        }
    }
}
