﻿using DSharpPlus.CommandsNext;

namespace Wheatly.Entities
{
    public class User(CommandContext ctx)
    {
        public ulong UserId { get; set; } = ctx.User.Id;
        public string UserName { get; set; } = ctx.User.Username;
        public string? DisplayName { get; set; } = ctx.Guild?.Name;
    }
}
