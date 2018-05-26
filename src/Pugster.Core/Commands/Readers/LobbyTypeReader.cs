using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster
{
    public class LobbyTypeReader : TypeReader
    {
        public override async Task<TypeReaderResult> Read(ICommandContext context, string input, IServiceProvider services)
        {
            var root = (RootController)services.GetService(typeof(RootController));
            var lobbies = await root.FindLobbiesAsync(input);

            string errorMsg = $"A lobby by the name of `{input}` does not exist";

            if (lobbies != null && lobbies.Count == 1)
                return TypeReaderResult.FromSuccess(lobbies.First());
            if (lobbies.Count > 1)
                errorMsg += $", did you mean:\n{string.Join(", ", lobbies.Select(x => x.Name))}";
            
            return TypeReaderResult.FromError(CommandError.ObjectNotFound, errorMsg);
        }
    }
}
