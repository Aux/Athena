using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Pugster
{
    public class HeroTypeReader : TypeReader
    {
        public override async Task<TypeReaderResult> Read(ICommandContext context, string input, IServiceProvider services)
        {
            var overwatch = (OverwatchController)services.GetService(typeof(OverwatchController));
            var hero = await overwatch.GetHeroAsync(input);

            if (hero != null) return TypeReaderResult.FromSuccess(hero);
            return TypeReaderResult.FromError(CommandError.ObjectNotFound, $"A hero by the name of `{input}` does not exist.");
        }
    }
}
