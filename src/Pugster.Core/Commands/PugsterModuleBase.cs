using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Pugster
{
    public abstract class PugsterModuleBase : ModuleBase<PugsterCommandContext>
    {
        public Task ReplyReactionAsync(string text)
            => ReplyReactionAsync(Emote.Parse(text));
        public Task ReplyReactionAsync(IEmote emote)
            => Context.Message.AddReactionAsync(emote);
    }
}
