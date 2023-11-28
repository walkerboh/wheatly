using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Wheatly.Database.Entities;

namespace Wheatly.Database
{
    public class WheatlyContext(IConfiguration configuration) : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var db = configuration.GetConnectionString("WheatlyContext");
            optionsBuilder.UseMySql(db, ServerVersion.AutoDetect(db));
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
    }
}
