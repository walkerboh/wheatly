using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

            await Host.CreateDefaultBuilder()
                .UseSerilog()
                .UseConsoleLifetime()
                .ConfigureServices(ConfigureServices)
                .RunConsoleAsync();

            await Log.CloseAndFlushAsync();
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddLogging(logging => logging.ClearProviders().AddSerilog())
                .AddOptions()
                .Configure<DiscordClientConfiguration>(hostContext.Configuration.GetSection("DiscordClientSettings"))
                .AddDbContext<WheatlyContext>()
                .AddTransient<QuestionsService>()
                .AddTransient<SuggestionsService>()
                .AddHostedService<WheatlyService>();
        }
    }
}
