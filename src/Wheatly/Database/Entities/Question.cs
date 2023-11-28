namespace Wheatly.Database.Entities
{
    public class Question(ulong userId, string userName, string? displayName, string text)
    {
        public int Id { get; set; }
        public ulong UserId { get; set; } = userId;
        public string? UserName { get; set; } = userName;
        public string? DisplayName { get; set; } = displayName;
        public string? Text { get; set; } = text;
    }
}
