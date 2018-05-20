using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pugster
{
    [RequireOwner]
    public class OverwatchSecretModule : PugsterModuleBase
    {
        private readonly OverwatchController _overwatch;
        private readonly IConfiguration _config;

        public OverwatchSecretModule(OverwatchController overwatch, IConfiguration config)
        {
            _overwatch = overwatch;
            _config = config;
        }

        [Command("loaddefaultheroes")]
        public async Task LoadDefaultHeroesAsync()
        {
            var defaultHeroes = new List<Hero>();
            _config.GetSection("Heroes").Bind(defaultHeroes);

            await _overwatch.CreateHeroesAsync(defaultHeroes.ToArray());
            await ReplyAsync("Done");
        }

        [Command("showhero")]
        public async Task ShowHeroAsync(Hero hero)
        {
            var embed = new EmbedBuilder()
                .WithTitle($"{hero.Name} ({hero.Class.ToString()})")
                .WithDescription(hero.Description)
                .WithImageUrl(hero.ImageUrl);
            await ReplyAsync("", embed: embed);
        }
    }
}
