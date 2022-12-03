# nullable disable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using Advent.Models;
using Advent.Logic;

var host = CreateHostBuilder(args).Build();

Application app = host.Services.GetRequiredService<Application>();
app.Process();

static IHostBuilder CreateHostBuilder(string[] args)
{
    Config.Configure(args);
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices(
            (_, services) => services
                .AddSingleton<Application, Application>()
                .AddSingleton<RockPaperScissorsLogic, RockPaperScissorsLogic>()
        );
}

static class Config
{
    public static bool TestData { get; set; }
    public static void Configure(string[] args)
    {
        TestData = args.Any(a => a == "-t");
    }
}

class Application
{
    private ILogger<Application> _logger;
    private RockPaperScissorsLogic _rockPaperScissorsLogic;
    public Application(ILogger<Application> logger, RockPaperScissorsLogic rockPaperScissorsLogic)
    {
        _logger = logger;
        _rockPaperScissorsLogic = rockPaperScissorsLogic;
    }

    public void Process()
    {
        string dataFile = Config.TestData ? @"./IO/testinput.txt" : @"./IO/input.txt";
        _logger.LogInformation($"TestData: {Config.TestData}");
        _logger.LogInformation($"Data file to use: {dataFile}");
        string[] lines = System.IO.File.ReadAllLines(dataFile);
        Decimal total = 0;
        foreach (var line in lines)
        {
            total += (Decimal) _rockPaperScissorsLogic.EvaluateStrategy(line).StrategyScore;
        }
        Console.WriteLine($"The total score was {total}.");
        Console.Write("Press any key to exit.");
        try 
        {
            Console.ReadKey();
        }
        catch {}
    }
}