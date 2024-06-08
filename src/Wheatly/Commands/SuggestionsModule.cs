using DSharpPlus.Commands;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Commands.Trees.Metadata;
using System.ComponentModel;
using Wheatly.Extensions;
using Wheatly.Services;

namespace Wheatly.Commands
{
    [Command("suggestion"), TextAlias("s")]
    [Description("Commands about bot suggestions")]
    public class SuggestionsModule(SuggestionsService suggestionsService, ILogger<SuggestionsModule> logger)
    {
        [Command("submit"), TextAlias("s")]
        [DefaultGroupCommand]
        [Description("Submit a new bot suggestion")]
        public async Task SubmitSuggestion(CommandContext ctx, [RemainingText] [Description("Text of your suggestion")] string text)
        {
            logger.LogInformation("SubmitSuggestion triggered");
            if (string.IsNullOrEmpty(text))
            {
                await ctx.RespondAsync("Your suggestion seems to be empty...");
            }
            else
            {
                await suggestionsService.SubmitSuggestionAsync(ctx.ToUser(), text, DateTime.UtcNow);
                await ctx.RespondAsync("Thank you for your suggestion!");
            }
        }
    }
}
