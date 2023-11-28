using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Microsoft.Extensions.Logging;
using Wheatly.Extensions;
using Wheatly.Services;

namespace Wheatly.Commands
{
    [Group("suggestion"), Aliases("s")]
    [Description("Commands about bot suggestions")]
    public class SuggestionsModule(SuggestionsService suggestionsService, ILogger<SuggestionsModule> logger) : BaseCommandModule
    {
        [Command("submit"), Aliases("s")]
        [GroupCommand]
        [Description("Submit a new bot suggestion")]
        public async Task SubmitSuggestion(CommandContext ctx, [RemainingText] string text)
        {
            logger.LogInformation("SubmitSuggestion triggered");
            if (string.IsNullOrEmpty(text))
            {
                await ctx.RespondAsync("Your suggestion seems to be empty...");
            }
            else
            {
                await suggestionsService.SubmitSuggestion(ctx.ToUser(), text);
                await ctx.RespondAsync("Thank you for your suggestion!");
            }
        }
    }
}
