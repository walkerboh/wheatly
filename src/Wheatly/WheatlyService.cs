using DSharpPlus;
using DSharpPlus.CommandsNext;
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

            commands.RegisterCommands<QuestionsModule>();
            commands.RegisterCommands<SuggestionsModule>();
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
