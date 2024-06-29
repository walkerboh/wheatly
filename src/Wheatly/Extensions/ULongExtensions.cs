namespace Wheatly.Extensions
{
    public static class ULongExtensions
    {
        public static string ToChannelMention(this ulong channelId) => $"<#{channelId}>";
    }
}
