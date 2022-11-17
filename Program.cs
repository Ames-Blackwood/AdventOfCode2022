# nullable disable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = CreateHostBuilder(args).Build();

Application app = host.Services.GetRequiredService<Application>();

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices(
            (_, services) => services
                .AddSingleton<Application, Application>()
                .AddSingleton<IAppConfig, AppConfig>());
}

interface IAppConfig
{
    string Setting { get; }
}

class AppConfig : IAppConfig
{
    public string Setting { get; }

    public AppConfig(ILogger<AppConfig> logger)
    {
        logger.Log(LogLevel.Information, "AppConfig constructed");
    }
}

class Application
{
    readonly IAppConfig config;
    public Application(IAppConfig config, ILogger<Application> logger)
    {
        this.config = config;
        logger.Log(LogLevel.Information, "Application constructed");
    }
}