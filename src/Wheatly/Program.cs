using DSharpPlus;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Commands.Processors.TextCommands;
using DSharpPlus.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Wheatly.Configuration;
using Wheatly.Database;
using Wheatly.Services;

namespace Wheatly
{
    internal class Program
    {
        private static async Task Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder();

            builder.Host
                .UseSerilog()
                .UseConsoleLifetime()
                .ConfigureServices(ConfigureServices);

            var app = builder.Build();

            app.MapGet("/questions", async (WheatlyContext context) => await context.Questions.ToListAsync());

            await app.RunAsync();

            await Log.CloseAndFlushAsync();
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            var token = Environment.GetEnvironmentVariable("DiscordClientSettings__Token");

            if(string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine("Error: No discord token found.");
                Environment.Exit(1);
            }

            services.AddLogging(logging => logging.ClearProviders().AddSerilog())
                .AddOptions()
                .Configure<DiscordClientConfiguration>(hostContext.Configuration.GetSection("DiscordClientSettings"))
                .AddDiscordClient(token, DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents | TextCommandProcessor.RequiredIntents | SlashCommandProcessor.RequiredIntents)
                .AddDbContextFactory<WheatlyContext>()
                .AddTransient<QuestionsService>()
                .AddTransient<SuggestionsService>()
                .AddTransient<TeamNameService>()
                .AddSingleton<Random>()
                .AddHostedService<WheatlyService>();
        }
    }
}
