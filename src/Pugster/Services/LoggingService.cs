using Discord.Commands;
using Discord.WebSocket;
using NTwitch.Chat;
using NTwitch.Rest;
using System.Threading.Tasks;

namespace Pugster
{
    public class LoggingService : BaseLoggingService
    {
        private readonly DiscordSocketClient _discord;
        private readonly TwitchChatClient _twitchBot;
        private readonly TwitchRestClient _twitchChannel;
        private readonly CommandService _commands;
        
        public LoggingService(
            DiscordSocketClient discord,
            TwitchChatClient twitchBot,
            TwitchRestClient twitchChannel,
            CommandService commands) : base()
        {
            _discord = discord;
            _twitchBot = twitchBot;
            _twitchChannel = twitchChannel;
            _commands = commands;

            _discord.Log += OnLogAsync;
            _twitchBot.Log += OnLogAsync;
            _twitchChannel.Log += OnLogAsync;
            _commands.Log += OnLogAsync;
        }
        
        private Task OnLogAsync(Discord.LogMessage msg)
            => LogAsync(msg.Severity, msg.Source, msg.Exception?.ToString() ?? msg.Message);
        private Task OnLogAsync(NTwitch.LogMessage msg)
            => LogAsync(msg.Level, msg.Source, msg.Exception?.ToString() ?? msg.Message);
    }
}
