using Discord.Commands;
using Discord.WebSocket;
using NTwitch.Chat;
using NTwitch.Rest;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pugster
{
    public class LoggingService
    {
        private readonly DiscordSocketClient _discord;
        private readonly TwitchChatClient _twitchBot;
        private readonly TwitchRestClient _twitchChannel;
        private readonly CommandService _commands;

        private string _logDirectory { get; }
        private string _logFile => Path.Combine(_logDirectory, $"{DateTime.UtcNow.ToString("yyyy-MM-dd")}.txt");

        public LoggingService(
            DiscordSocketClient discord,
            TwitchChatClient twitchBot,
            TwitchRestClient twitchChannel,
            CommandService commands)
        {
            _logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");

            _discord = discord;
            _twitchBot = twitchBot;
            _twitchChannel = twitchChannel;
            _commands = commands;

            _discord.Log += OnLogAsync;
            _twitchBot.Log += OnLogAsync;
            _twitchChannel.Log += OnLogAsync;
            _commands.Log += OnLogAsync;
        }

        public Task LogAsync(object severity, string source, string message)
        {
            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);
            if (!File.Exists(_logFile))
                File.Create(_logFile).Dispose();

            string logText = $"{DateTime.UtcNow.ToString("hh:mm:ss")} [{severity}] {source}: {message}";
            File.AppendAllText(_logFile, logText + "\n");

            return Console.Out.WriteLineAsync(logText);
        }

        private Task OnLogAsync(Discord.LogMessage msg)
            => LogAsync(msg.Severity, msg.Source, msg.Exception?.ToString() ?? msg.Message);
        private Task OnLogAsync(NTwitch.LogMessage msg)
            => LogAsync(msg.Level, msg.Source, msg.Exception?.ToString() ?? msg.Message);
    }
}
