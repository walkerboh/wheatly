using Wheatly.Database;
using Wheatly.Database.Entities;
using Wheatly.Entities;

namespace Wheatly.Services
{
    public class SuggestionsService(WheatlyContext context)
    {
        private readonly WheatlyContext _context = context;

        public int GetUserCount(ulong userId) => _context.Suggestions.Count(s => s.UserId == userId);

        public async Task SubmitSuggestion(User user, string text)
        {
            var suggestion = new Suggestion(user.UserId, user.UserName, user.DisplayName, text);

            _context.Suggestions.Add(suggestion);
            await _context.SaveChangesAsync();
        }
    }
}
