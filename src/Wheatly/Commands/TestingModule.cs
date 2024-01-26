using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Wheatly.Commands
{
    public class TestingModule : BaseCommandModule
    {
        [Command("ping")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.RespondAsync("Pong!");
        }
    }
}
