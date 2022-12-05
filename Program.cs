# nullable disable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
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
                .AddSingleton<RucksackLogic, RucksackLogic>()
        );
}

static class Config
{
    public static bool TestData { get; set; }
    public static LogLevel logLevel { get; set; }
    public static void Configure(string[] args)
    {
        TestData = args.Any(a => a == "-t");
    }
}

class Application
{
    private ILogger<Application> _logger;
    private RucksackLogic _rucksackLogic;
    public Application(ILogger<Application> logger, RucksackLogic rucksackLogic)
    {
        _logger = logger;
        _rucksackLogic = rucksackLogic;
    }

    public void Process()
    {
        string dataFile = Config.TestData ? @"./IO/testinput.txt" : @"./IO/input.txt";
        _logger.LogInformation($"TestData: {Config.TestData}");
        _logger.LogInformation($"Data file to use: {dataFile}");
        string[] lines = System.IO.File.ReadAllLines(dataFile);
        Decimal total = 0;
        List<string> buffer = new List<string>();
        
        foreach (var line in lines)
        {
            buffer.Add(line);
            if (buffer.Count >= 3)
            {
                total += (Decimal) _rucksackLogic.FindPriorityOfBadge(buffer);
                buffer = new List<string>();
            }
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