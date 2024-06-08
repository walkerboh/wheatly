using DSharpPlus.Commands;
using Wheatly.Entities;

namespace Wheatly.Extensions
{
    public static class CommandContextExtensions
    {
        public static User ToUser(this CommandContext ctx) => new(ctx);
    }
}
