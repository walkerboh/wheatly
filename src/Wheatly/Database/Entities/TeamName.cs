namespace Wheatly.Database.Entities
{
    public class TeamName(string submittedName, ulong userId, string? displayName, DateTime submittedAt)
    {
        public int Id { get; set; }
        public string SubmittedName { get; set; } = submittedName;
        public ulong UserId { get; set; } = userId;
        public string? DisplayName { get; set; } = displayName;
        public DateTime SubmittedAt { get; set; } = submittedAt;
    }
}
