using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace Pugster
{
    public class BattleTagTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> Read(ICommandContext context, string input, IServiceProvider services)
        {
            var tag = BattleTag.Parse(input);
            if (tag.IsValid)
                return Task.FromResult(TypeReaderResult.FromSuccess(tag));
            else
                return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, $"`{input}` is not a valid battle tag"));
        }
    }
}
