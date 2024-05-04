using Microsoft.EntityFrameworkCore;
using Wheatly.Database.Entities;

namespace Wheatly.Database
{
    public class WheatlyContext(IConfiguration configuration) : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var db = configuration.GetConnectionString("WheatlyContext");
            optionsBuilder.UseNpgsql(db);
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
    }
}
