using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Wheatly.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class RequireChannelAttribute : CheckBaseAttribute
    {
        public ulong ChannelId { get; private set; }

        public RequireChannelAttribute(ulong channelId)
        {
            ChannelId = channelId;
        }

        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            return Task.FromResult(ctx.Channel.Id == ChannelId);
        }
    }
}
