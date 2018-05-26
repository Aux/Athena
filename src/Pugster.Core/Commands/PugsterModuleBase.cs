using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Pugster
{
    public abstract class PugsterModuleBase : ModuleBase<PugsterCommandContext>
    {
        public Task ReplySuccessAsync()
            => ReplyReactionAsync(new Emoji("👍"));
        public Task ReplyReactionAsync(IEmote emote)
            => Context.Message.AddReactionAsync(emote);
        public Task ReplyEmbedAsync(Embed embed)
            => ReplyAsync("", embed: embed);
    }
}
