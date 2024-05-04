namespace Wheatly.Database.Entities
{
    public class Question(ulong userId, string userName, string? displayName, string text, DateTime submittedAt)
    {
        public int Id { get; set; }
        public ulong UserId { get; set; } = userId;
        public string? UserName { get; set; } = userName;
        public string? DisplayName { get; set; } = displayName;
        public string? Text { get; set; } = text;
        public DateTime SubmittedAt { get; set; } = submittedAt;
    }
}
