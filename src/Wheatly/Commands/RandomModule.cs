using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Wheatly.GameRandoms;

namespace Wheatly.Commands
{
    public class RandomModule(Random random) : BaseCommandModule
    {
        [Command("random")]
        public async Task Random(CommandContext ctx)
        {
            await ctx.RespondAsync($"Your random value is {random.Next()}");
        }

        [Command("random")]
        public async Task RandomInt(CommandContext ctx, int max)
        {
            // Normal people are 1-indexed
            var value = random.Next(max) + 1;
            await ctx.RespondAsync($"Your random value is {value}.");
        }

        [Command("random")]
        public async Task RandomGame(CommandContext ctx, string game)
        {
            var gameRandom = GameRandomFactory.GetGameRandom(game);
            if (gameRandom is null)
            {
                await ctx.RespondAsync("Game supplied is not supported. Please check spelling or submit a request for it to be added.");
            }
            else
            {
                await ctx.RespondAsync(gameRandom.GetValues(random));
            }
        }
    }
}
