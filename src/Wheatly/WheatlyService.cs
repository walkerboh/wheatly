using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Exceptions;
using DSharpPlus.Commands.Processors.TextCommands;
using Microsoft.Extensions.Options;
using Wheatly.Configuration;
using Wheatly.ContextChecks;
using Wheatly.Extensions;

namespace Wheatly
{
    public class WheatlyService : IHostedService
    {
        private ILogger<WheatlyService> Logger { get; set; }
        private DiscordClientConfiguration DiscordClientOptions { get; set; }
        private DiscordClient Client { get; set; }

        public WheatlyService(ILogger<WheatlyService> logger, IOptions<DiscordClientConfiguration> options, DiscordClient discordClient)
        {
            Logger = logger;
            DiscordClientOptions = options.Value;
            Client = discordClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Client setup starting");

            CommandsExtension commandsExtension = Client.UseCommands(new()
            {
                DebugGuildId = DiscordClientOptions.DebugGuildId ?? 0,
                UseDefaultCommandErrorHandler = false
            });

            commandsExtension.CommandExecuted += CommandsExtension_CommandExecuted;
            commandsExtension.CommandErrored += CommandsExtension_CommandErrored;

            commandsExtension.AddCommands(typeof(Program).Assembly);

            commandsExtension.AddCheck<RequiredChannelCheck>();

            TextCommandProcessor textCommandProcessor = new(new());

            await commandsExtension.AddProcessorAsync(textCommandProcessor);

            Logger.LogInformation("Wheatly is connecting");
            await Client.ConnectAsync();
        }

        private async Task CommandsExtension_CommandErrored(CommandsExtension sender, DSharpPlus.Commands.EventArgs.CommandErroredEventArgs args)
        {
            if (args.Exception is ChecksFailedException checksFailedException)
            {
                foreach (var failedCheck in checksFailedException.Errors)
                {
                    if (failedCheck.ContextCheckAttribute is RequiredChannelAttribute channelAttribute)
                    {
                        if (channelAttribute.AllowedGuildChannels.TryGetValue(args.Context.Guild?.Id ?? 0, out var channelId))
                        {
                            await args.Context.RespondAsync($"This command is not uable in this channel, try: {channelId.ToChannelMention()}");
                        }
                        else
                        {
                            await args.Context.RespondAsync($"This command is not usable in this server.");
                        }

                        return;
                    }
                }
            }

            if (args.Exception is CommandNotFoundException)
            {
                await args.Context.RespondAsync("That doesn't seem like an actual command...");
                Logger.LogError(args.Exception, "Error finding command");
                return;
            }

            await args.Context.RespondAsync("Sorry, an error occurred. Please try again later... or yell at Evan.");
            Logger.LogError(args.Exception, "Error executing command: {CommandName}", args.Context.Command.Name);
        }

        private Task CommandsExtension_CommandExecuted(CommandsExtension sender, DSharpPlus.Commands.EventArgs.CommandExecutedEventArgs args)
        {
            Logger.LogInformation("Command {command} executed", args.Context.Command.Name);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Wheatly is disconnecting");
            await Client.DisconnectAsync();
        }
    }
}
