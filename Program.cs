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
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices(
            (_, services) => services
                .AddSingleton<Application, Application>()
                .AddSingleton<RockPaperScissorsLogic, RockPaperScissorsLogic>()
        );
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
        string[] lines = System.IO.File.ReadAllLines(@"./IO/input.txt");
        Decimal total = 0;
        foreach (var line in lines)
        {
            total += (Decimal) _rockPaperScissorsLogic.EvaluateStrategy(line).StrategyScore;
        }
        Console.WriteLine($"The total score was {total}.");
        Console.Write("Press any key to exit.");
        Console.ReadKey();
    }
}