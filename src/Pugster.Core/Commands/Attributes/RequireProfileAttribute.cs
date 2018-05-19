using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Pugster
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireProfileAttribute : PreconditionAttribute
    {
        public override async Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var profiles = (ProfileController)services.GetService(typeof(ProfileController));
            bool profileExists = await profiles.ProfileExistsAsync(context.User.Id);

            if (profileExists)
                return PreconditionResult.FromSuccess();
            else
                return PreconditionResult.FromError("You must create a profile before using this command");
        }
    }
}
