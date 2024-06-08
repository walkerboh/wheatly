using DSharpPlus.Commands;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Commands.Trees.Metadata;
using System.ComponentModel;
using Wheatly.ContextChecks;
using Wheatly.Extensions;
using Wheatly.Services;

namespace Wheatly.Commands
{
    [Command("question"), TextAlias("q")]
    [Description("Commands about Extra Life questions. Can only be run in #interview-questions")]
    public class QuestionsModule(QuestionsService questionsService, ILogger<QuestionsModule> logger)
    {
        [Command("submit"), TextAlias("s")]
        [DefaultGroupCommand]
        [Description("Submit a new potential question for the Extra Life interview")]
        [RequiredChannel("251851170988032000-1249110617738973214", "123917893447450628-1236442191161983027")]
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

        [Command("count"), TextAlias("c")]
        [Description("Get the total count of submitted questions")]
        public async Task QuestionCount(CommandContext ctx)
        {
            logger.LogInformation("QuestionCount triggered");
            var count = await questionsService.GetTotalCountAsync();
            await ctx.RespondAsync($"There are {count} questions total.");
        }

        [Command("myCount"), TextAlias("mc")]
        [Description("Get the count of questions you have submitted")]
        public async Task MyQuestionCount(CommandContext ctx)
        {
            logger.LogInformation("MyQuestionCount triggered");
            var count = await questionsService.GetUserCountAsync(ctx.User.Id);
            await ctx.RespondAsync($"You have submitted {count} questions.");
        }
    }
}
