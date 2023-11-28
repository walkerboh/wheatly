using Wheatly.Database;
using Wheatly.Database.Entities;
using Wheatly.Entities;

namespace Wheatly.Services
{
    public class QuestionsService(WheatlyContext context)
    {
        private readonly WheatlyContext _context = context;

        public int GetTotalCount() => _context.Questions.Count();

        public int GetUserCount(ulong userId) => _context.Questions.Count(q => q.UserId == userId);

        public async Task SubmitQuestion(User user, string text)
        {
            var question = new Question(user.UserId, user.UserName, user.DisplayName, text);

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }
    }
}
