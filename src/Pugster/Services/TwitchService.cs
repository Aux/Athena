using NTwitch.Chat;
using NTwitch.Rest;
using System.Threading.Tasks;

namespace Pugster
{
    public class TwitchService
    {
        private readonly TwitchChatClient _twitchBot;
        private readonly TwitchRestClient _twitchChannel;

        public TwitchService(TwitchChatClient twitchBot, TwitchRestClient twitchChannel)
        {
            _twitchBot = twitchBot;
            _twitchChannel = twitchChannel;

            _twitchBot.MessageReceived += OnMessageReceivedAsync;
        }

        public async Task StartAsync()
        {
            await _twitchBot.JoinChannelAsync(_twitchChannel.CurrentUser.Name);
        }

        private async Task OnMessageReceivedAsync(ChatMessage arg)
        {
            await Task.Delay(0);
        }
    }
}
