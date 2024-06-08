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
            services.AddLogging(logging => logging.ClearProviders().AddSerilog())
                .AddOptions()
                .Configure<DiscordClientConfiguration>(hostContext.Configuration.GetSection("DiscordClientSettings"))
                .AddDbContextFactory<WheatlyContext>()
                .AddTransient<QuestionsService>()
                .AddTransient<SuggestionsService>()
                .AddTransient<TeamNameService>()
                .AddSingleton<Random>()
                .AddHostedService<WheatlyService>();
        }
    }
}
