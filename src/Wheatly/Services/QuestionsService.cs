using Microsoft.EntityFrameworkCore;
using Wheatly.Database;
using Wheatly.Database.Entities;
using Wheatly.Entities;

namespace Wheatly.Services
{
    public class QuestionsService(IDbContextFactory<WheatlyContext> dbFactory)
    {
        public async Task<int> GetTotalCountAsync()
        {
            await using var context = await dbFactory.CreateDbContextAsync();
            return context.Questions.Count();
        }

        public async Task<int> GetUserCountAsync(ulong userId)
        {
            await using var context = await dbFactory.CreateDbContextAsync();
            return context.Questions.Count(q => q.UserId == userId);
        }

        public async Task SubmitQuestionAsync(User user, string text, DateTime submittedAt)
        {
            await using var context = await dbFactory.CreateDbContextAsync();
            var question = new Question(user.UserId, user.UserName, user.DisplayName, text, submittedAt);

            context.Questions.Add(question);
            await context.SaveChangesAsync();
        }
    }
}
