﻿# nullable disable
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
                .AddSingleton<CrateStackLogic, CrateStackLogic>()
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
    private CrateStackLogic _CrateStackLogic;
    public Application(ILogger<Application> logger, CrateStackLogic CrateStackLogic)
    {
        _logger = logger;
        _CrateStackLogic = CrateStackLogic;
    }

    public void Process()
    {
        string dataFile = Config.TestData ? @"./IO/testinput.txt" : @"./IO/input.txt";
        _logger.LogInformation($"TestData: {Config.TestData}");
        _logger.LogInformation($"Data file to use: {dataFile}");
        string[] lines = System.IO.File.ReadAllLines(dataFile);

        List<string> buffer = new List<string>();

        var readingStackBlock = true;
        
        foreach (var line in lines)
        {
            if (readingStackBlock) {
                if (line == "")
                {
                    readingStackBlock = false;
                    _CrateStackLogic.ModelStack(buffer);
                    break;
                }
                else
                {
                    buffer.Add(line);
                }
            }
            else
            {
                _CrateStackLogic.UpdateStack(line);
            }
        }
        var output = _CrateStackLogic.GetTopItems();
        
        Console.WriteLine($"The top items on each stack in order are \"{output}\".");
        Console.Write("Press any key to exit.");
        try 
        {
            Console.ReadKey();
        }
        catch {}
    }
}