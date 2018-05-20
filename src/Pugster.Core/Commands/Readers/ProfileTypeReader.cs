using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Pugster
{
    public class ProfileTypeReader : TypeReader
    {
        public override async Task<TypeReaderResult> Read(ICommandContext context, string input, IServiceProvider services)
        {
            var profiles = (ProfileController)services.GetService(typeof(ProfileController));

            Profile profile;
            var battleTag = BattleTag.Parse(input);
            if (battleTag.IsValid)
                profile = await profiles.GetProfileAsync(battleTag);
            else if (MentionUtils.TryParseUser(input, out ulong userId))
                profile = await profiles.GetProfileAsync(userId);
            else
                profile = await profiles.GetProfileAsync(input);

            if (profile != null) return TypeReaderResult.FromSuccess(profile);
            return TypeReaderResult.FromError(CommandError.ObjectNotFound, $"A profile by the name of `{input}` does not exist.");
        }
    }
}
