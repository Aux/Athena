using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTwitch.Chat;
using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace Pugster
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("_configuration.json")
                .AddJsonFile("_default_heroes.json");
            Configuration = builder.Build();
        }

        public static async Task RunAsync(string[] args)
        {
            var startup = new Startup(args);
            await startup.RunAsync();
        }

        public async Task RunAsync()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            await provider.GetRequiredService<StartupService>().StartAsync();

            provider.GetRequiredService<LoggingService>();
            provider.GetRequiredService<CommandHandler>();

            await Task.Delay(-1);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = Discord.LogSeverity.Verbose,
                    MessageCacheSize = 1000
                }))
                .AddSingleton(new TwitchChatClient(new TwitchChatConfig
                {
                    LogLevel = NTwitch.LogSeverity.Verbose,
                    MessageCacheSize = 100
                }))
                .AddSingleton(new TwitchRestClient(new TwitchRestConfig
                {
                    LogLevel = NTwitch.LogSeverity.Verbose
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = Discord.LogSeverity.Verbose,
                    CaseSensitiveCommands = false,
                    DefaultRunMode = RunMode.Async
                }))
                .AddDbContext<RootDatabase>()
                .AddDbContext<OverwatchDatabase>()
                .AddTransient<ProfileController>()
                .AddTransient<OverwatchController>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<StartupService>()
                .AddSingleton<LoggingService>()
                .AddSingleton(Configuration);
        }
    }
}
