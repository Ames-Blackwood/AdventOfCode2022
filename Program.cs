# nullable disable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;

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
    public Application(ILogger<Application> logger)
    {
        _logger = logger;
    }

    public void Process()
    {
        string dataFile = Config.TestData ? @"./IO/testinput.txt" : @"./IO/input.txt";
        _logger.LogInformation($"TestData: {Config.TestData}");
        _logger.LogInformation($"Data file to use: {dataFile}");
        
        StreamReader reader = new StreamReader(dataFile);

        const int matchSize = 14;
        int position = 0;
        var markerFound = false;        
        List<char> evalList = new List<char>();
        var listCount = 0;

        while (!markerFound && reader.Peek() >= 0)
        {
            position++;
            evalList.Add((char)reader.Read());
            listCount = evalList.Count();
            if (listCount >= matchSize)
            {
                if (listCount > matchSize) evalList.RemoveRange(0,listCount-matchSize);
                if (evalList.Distinct().Count() == matchSize ) markerFound = true;
            }
        }

        reader.Close();

        if (!markerFound) throw new Exception("Marker could not be found.");

        Console.WriteLine($"The first start-of-packet marker is at \"{position}\".");
        Console.Write("Press any key to exit.");
        try 
        {
            Console.ReadKey();
        }
        catch {}
    }
}