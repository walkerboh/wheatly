using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Wheatly.Extensions;
using Wheatly.Services;

namespace Wheatly.Commands
{
    [Group("teamName"), Aliases("tn")]
    [Description("Commands to get random team names")]
    public class TeamNameModule(TeamNameService teamNameService, ILogger<TeamNameModule> logger) : BaseCommandModule
    {
        [Command("submit"), Aliases("s")]
        [Description("Submit a name to the pool of team names")]
        public async Task SubmitTeamName(CommandContext ctx, [RemainingText] string text)
        {
            if(string.IsNullOrEmpty(text))
            {
                await ctx.RespondAsync("Submit with no text");
            }
            else
            {
                await teamNameService.SubmitTeamNameAsync(ctx.ToUser(), text, DateTime.UtcNow);
                await ctx.RespondAsync("Team name submitted.");
            }
        }

        [GroupCommand]
        [Command("get"), Aliases("g")]
        [Description("Get a random team name from the pool of submitted names")]
        public async Task GetTeamName(CommandContext ctx, [Description("Optional maximum length for the selected team name")] int? maxLength = null)
        {
            var teamName = await teamNameService.GetRandomTeamNameAsync(maxLength);

            if (teamName is null)
            {
                if (maxLength.HasValue)
                {
                    await ctx.RespondAsync("Sorry! No team names that fit that length.");
                }
                else
                {
                    await ctx.RespondAsync("Sorry! No team names submitted yet. Why don't you fix that?");
                }
            }
            else
            {
                await ctx.RespondAsync($"Here, try this name: {teamName.SubmittedName}. Submitted by: {teamName.DisplayName}");
            }
        }
    }
}
