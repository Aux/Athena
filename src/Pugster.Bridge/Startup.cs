using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Pugster.Bridge
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        private readonly DiscordHandler _discord;

        public Startup(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddCommandLine(args)
                .AddYamlFile("_configuration.yml")
                .Build();

            _discord = new DiscordHandler(Configuration);
        }

        public async Task RunAsync()
        {
            await _discord.RunAsync();

            await Task.Delay(-1);
        }
    }
}
