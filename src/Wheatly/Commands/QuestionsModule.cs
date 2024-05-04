using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Wheatly.Extensions;
using Wheatly.Services;

namespace Wheatly.Commands
{
    [Group("question"), Aliases("q")]
    [Description("Commands about Extra Life questions")]
    public class QuestionsModule(QuestionsService questionsService, ILogger<QuestionsModule> logger) : BaseCommandModule
    {
        [Command("submit"), Aliases("s")]
        [GroupCommand]
        [Description("Submit a new potential question for the Extra Life interview")]
        public async Task SubmitQuestion(CommandContext ctx, [RemainingText] [Description("Text of your suggested question")] string text)
        {
            logger.LogInformation("SubmitQuestion triggered");
            if (string.IsNullOrEmpty(text))
            {
                await ctx.RespondAsync("Your question doesn't seem to contain a question...");
            }
            else
            {
                await questionsService.SubmitQuestionAsync(ctx.ToUser(), text, DateTime.UtcNow);
                await ctx.RespondAsync("Thank you for your question!");
            }
        }

        [Command("count"), Aliases("c")]
        [Description("Get the total count of submitted questions")]
        public async Task QuestionCount(CommandContext ctx)
        {
            logger.LogInformation("QuestionCount triggered");
            var count = await questionsService.GetTotalCountAsync();
            await ctx.RespondAsync($"There are {count} questions total.");
        }

        [Command("myCount"), Aliases("mc")]
        [Description("Get the count of questions you have submitted")]
        public async Task MyQuestionCount(CommandContext ctx)
        {
            logger.LogInformation("MyQuestionCount triggered");
            var count = await questionsService.GetUserCountAsync(ctx.User.Id);
            await ctx.RespondAsync($"You have submitted {count} questions.");
        }
    }
}
