using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Wheatly.Commands;
using Wheatly.Configuration;

namespace Wheatly
{
    public class WheatlyService : IHostedService
    {
        private ILogger<WheatlyService> Logger { get; set; }
        private DiscordClient DiscordClient { get; set; }

        public WheatlyService(IServiceProvider provider, ILogger<WheatlyService> logger, IOptions<DiscordClientConfiguration> options)
        {
            Logger = logger;

            var clientConfig = options.Value;

            if(string.IsNullOrEmpty(clientConfig.Token))
            {
                throw new Exception("ClienfConfig.Token is missing");
            }

            if(clientConfig.Prefixes == null || clientConfig.Prefixes.Length == 0)
            {
                throw new Exception("ClientConfig.Prefixes is null or empty");
            }

            DiscordClient = new(new()
            {
                Token = clientConfig.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents,
                LoggerFactory = new LoggerFactory().AddSerilog()
            });

            var commands = DiscordClient.UseCommandsNext(new()
            {
                StringPrefixes = clientConfig.Prefixes,
                Services = provider
            });

            commands.CommandErrored += Commands_CommandErrored;

            commands.RegisterCommands<QuestionsModule>();
            commands.RegisterCommands<SuggestionsModule>();
            commands.RegisterCommands<TestingModule>();
        }

        private async Task Commands_CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs args)
        {
            await args.Context.RespondAsync("Sorry, an error occurred. Please try again later... or yell at Evan.");
            Logger.LogError(args.Exception, "Error executing command {CommandName}", args?.Command?.Name);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Wheatly is connecting");
            await DiscordClient.ConnectAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Wheatly is disconnecting");
            await DiscordClient.DisconnectAsync();
        }
    }
}
