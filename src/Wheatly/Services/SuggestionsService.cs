using Microsoft.EntityFrameworkCore;
using Wheatly.Database;
using Wheatly.Database.Entities;
using Wheatly.Entities;

namespace Wheatly.Services
{
    public class SuggestionsService(IDbContextFactory<WheatlyContext> dbFactory)
    {
        public async Task<int> GetUserCountAsync(ulong userId)
        {
            await using var context = await dbFactory.CreateDbContextAsync();
            return context.Suggestions.Count(s => s.UserId == userId);
        }

        public async Task SubmitSuggestionAsync(User user, string text, DateTime submittedAt)
        {
            await using var context = await dbFactory.CreateDbContextAsync();
            var suggestion = new Suggestion(user.UserId, user.UserName, user.DisplayName, text, submittedAt);

            context.Suggestions.Add(suggestion);
            await context.SaveChangesAsync();
        }
    }
}
