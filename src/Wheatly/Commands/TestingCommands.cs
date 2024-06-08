using DSharpPlus.Commands;

namespace Wheatly.Commands
{
    public class TestingCommands
    {
        [Command("ping")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.RespondAsync($"Pong! Current latency is {ctx.Client.Ping}ms");
        }
    }
}
