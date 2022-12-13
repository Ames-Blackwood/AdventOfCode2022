# nullable disable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using Advent.Logic;
using Advent.Models;

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
                .AddSingleton<FileSystemLogic, FileSystemLogic>()
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
    private FileSystemLogic _FileSystemLogic;
    public Application(ILogger<Application> logger, FileSystemLogic FileSystemLogic)
    {
        _logger = logger;
        _FileSystemLogic = FileSystemLogic;
    }

    public void Process()
    {
        string dataFile = Config.TestData ? @"./IO/testinput.txt" : @"./IO/input.txt";
        _logger.LogInformation($"TestData: {Config.TestData}");
        _logger.LogInformation($"Data file to use: {dataFile}");
        string[] lines = System.IO.File.ReadAllLines(dataFile);
        
        foreach (var line in lines)
        {
            _FileSystemLogic.TryParseEntry(line);
        }

        Console.WriteLine("The inferred filesystem:");
        _FileSystemLogic.WriteFolderContents();
        
        Console.Write("Press any key to exit.");
        try 
        {
            Console.ReadKey();
        }
        catch {}
    }
}