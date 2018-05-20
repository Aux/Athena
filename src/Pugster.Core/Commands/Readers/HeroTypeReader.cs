using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster
{
    public class HeroTypeReader : TypeReader
    {
        public override async Task<TypeReaderResult> Read(ICommandContext context, string input, IServiceProvider services)
        {
            var overwatch = (OverwatchController)services.GetService(typeof(OverwatchController));
            var heroes = await overwatch.FindHeroesAsync(input, 2);

            string errorMsg = $"A hero by the name of `{input}` does not exist";

            if (heroes != null && heroes.Count == 1)
                return TypeReaderResult.FromSuccess(heroes.First());
            if (heroes.Count > 1)
                errorMsg += $", did you mean:\n{string.Join(", ", heroes.Select(x => x.Name))}";

            return TypeReaderResult.FromError(CommandError.ObjectNotFound, errorMsg);
        }
    }
}
