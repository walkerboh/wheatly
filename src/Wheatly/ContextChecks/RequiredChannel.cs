using DSharpPlus.Commands;
using DSharpPlus.Commands.ContextChecks;

namespace Wheatly.ContextChecks
{
    public class RequiredChannelAttribute : ContextCheckAttribute
    {
        public Dictionary<ulong, ulong> AllowedGuildChannels { get; init; }
        public RequiredChannelAttribute(params string[] guildChannelStrings)
        {
            AllowedGuildChannels = guildChannelStrings.Select(s => s.Split('-')).ToDictionary(k => ulong.Parse(k[0]), v => ulong.Parse(v[1]));
        }
    }

    public class RequiredChannelCheck : IContextCheck<RequiredChannelAttribute>
    {
        public ValueTask<string?> ExecuteCheckAsync(RequiredChannelAttribute attribute, CommandContext context)
        {
            if(!attribute.AllowedGuildChannels.TryGetValue(context.Guild?.Id ?? 0, out var allowedChannelId))
            {
                return ValueTask.FromResult<string?>("The executed command is not allowed in this server.");
            }
            else if(context.Channel?.Id != allowedChannelId)
            {
                return ValueTask.FromResult<string?>("The executed command is not allowed in this channel.");
            }

            return ValueTask.FromResult<string?>(null);
        }
    }
}
