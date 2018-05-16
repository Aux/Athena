using System.Threading.Tasks;

namespace Pugster
{
    class Program
    {
        static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}
