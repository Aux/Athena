using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster.Modules
{
    [Group("clean")]
    [RequireContext(ContextType.Guild)]
    [RequireBotPermission(GuildPermission.ManageMessages)]
    [RequireUserPermission(ChannelPermission.ManageMessages)]
    public class CleanAdminModule : PugsterModuleBase
    {
        private SocketTextChannel TextChannel 
            => Context.Channel as SocketTextChannel;

        private async Task<IEnumerable<IMessage>> GetMessagesAsync(int history, Func<IEnumerable<IMessage>, IEnumerable<IMessage>> action = null)
        {
            var msgs = await Context.Channel.GetMessagesAsync(history).Flatten();
            action?.Invoke(msgs);
            return msgs;
        }

        [Command("all")]
        [Summary("Clean all messages in recent history")]
        public async Task AllAsync([Range(1, 1000)]int history = 25)
        {
            var messages = await GetMessagesAsync(history, x => x);

            await Context.Channel.DeleteMessagesAsync(messages);
            await ReplyAsync($"Cleaned **{history}** message(s) in {TextChannel.Mention}");
        }

        [Command("user")]
        [Summary("Clean all messages by the specified user in recent history")]
        public async Task CleanUserAsync(SocketGuildUser user, [Range(1, 1000)]int history = 25)
        {
            var messages = await GetMessagesAsync(history, msgs => msgs.Where(x => x.Author.Id == user.Id));

            await Context.Channel.DeleteMessagesAsync(messages);
            await ReplyAsync($"Cleaned **{messages.Count()}** message(s) in {TextChannel.Mention} by {user.Mention}");
        }

        [Command("contains")]
        [Summary("Clean all messages containing the specified text in recent history")]
        public async Task CleanContainsAsync(string match, [Range(1, 1000)]int history = 25)
        {
            var messages = await GetMessagesAsync(history, msgs => msgs.Where(x => x.Content.ToLower().Contains(match.ToLower())));

            await Context.Channel.DeleteMessagesAsync(messages);
            await ReplyAsync($"Cleaned **{messages.Count()}** message(s) in {TextChannel.Mention} containing the specified text");
        }

        [Command("duplicates")]
        [Summary("Clean all duplicate messages in recent history")]
        public async Task DuplicatesAsync([Range(1, 1000)]int history = 25)
        {
            var messages = await GetMessagesAsync(history, msgs => (msgs.GroupBy(x => x.Content).Where(x => x.Count() > 1)).SelectMany(x => x.Skip(1)));

            await Context.Channel.DeleteMessagesAsync(messages);
            await ReplyAsync($"Cleaned **{messages.Count()}** duplicate message(s) in {TextChannel.Mention}");
        }
    }
}
