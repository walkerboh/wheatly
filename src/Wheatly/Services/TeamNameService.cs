using Microsoft.EntityFrameworkCore;
using Wheatly.Database;
using Wheatly.Database.Entities;
using Wheatly.Entities;
using Wheatly.Extensions;

namespace Wheatly.Services
{
    public class TeamNameService(IDbContextFactory<WheatlyContext> dbFactory, Random rng)
    {
        public async Task SubmitTeamNameAsync(User user, string text, DateTime submittedAt)
        {
            await using var context = await dbFactory.CreateDbContextAsync();
            var teamName = new TeamName(text, user.UserId, user.DisplayName ?? user.UserName, submittedAt);

            context.TeamNames.Add(teamName);
            await context.SaveChangesAsync();
        }

        public async Task<TeamName?> GetRandomTeamNameAsync(int? maxLength)
        {
            await using var context = await dbFactory.CreateDbContextAsync();
            IEnumerable<TeamName> names = context.TeamNames;

            if(!names.Any())
            {
                return null;
            }

            if(maxLength.HasValue)
            {
                names = names.Where(n => n.SubmittedName.Length <= maxLength.Value);
            }

            return names.RandomElement(rng);
        }
    }
}
