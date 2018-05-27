using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster.Modules
{
    public class CleanModule : PugsterModuleBase
    {
        [Command("clean")]
        [Summary("Delete the bot's most recent messages")]
        [RequireContext(ContextType.Guild)]
        public async Task CleanAsync()
        {
            var msgs = Context.Channel.GetCachedMessages(100).Where(x => x.Author.Id == Context.Client.CurrentUser.Id);

            if (Context.Guild.CurrentUser.GuildPermissions.ManageMessages)
                await Context.Channel.DeleteMessagesAsync(msgs);
            else
            {
                foreach (var msg in msgs)
                    await msg.DeleteAsync();
            }

            await ReplyAsync($"Cleaned **{msgs.Count()}** of my message(s)");
        }
    }
}
