﻿# nullable disable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;

var host = CreateHostBuilder(args).Build();

Application app = host.Services.GetRequiredService<Application>();
app.Process();

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices(
            (_, services) => services
                .AddSingleton<Application, Application>());
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
        string[] lines = System.IO.File.ReadAllLines(@"./input.txt");
        Dictionary<int, decimal> elfCaloryTotal = new Dictionary<int, decimal>();
        decimal maxCalories = 0;
        decimal currentCalories = 0;
        int maxCarryingElf = 1;
        int currentElf = 1;
        decimal value;
        bool isValue;
        List<decimal> allCaloriesList = new List<decimal>();

        foreach (var line in lines)
        {
            isValue = Decimal.TryParse(line, out value);
            if (!isValue)
            {
                allCaloriesList.Add(currentCalories);
                elfCaloryTotal.Add(currentElf,currentCalories);
                if (currentCalories > maxCalories)
                {
                    maxCarryingElf = currentElf;
                    maxCalories = currentCalories;
                }
                currentElf++;
                currentCalories = 0;
            }
            else {
                currentCalories += value;
            }
        }

        elfCaloryTotal.Add(currentElf,currentCalories);
        if (currentCalories > maxCalories)
        {
            maxCarryingElf = currentElf;
            maxCalories = currentCalories;
        }

        List<Decimal> top3 = allCaloriesList.OrderBy(inP => -inP).Take(3).ToList();
        foreach (var top in top3){
            Console.WriteLine(top);
        }
        Decimal result = top3.Sum();
        Console.WriteLine($"Calories carried by the 3 elves with the most by List: {result}");
        Console.Write("Press any key to exit.");
        Console.ReadKey();
    }
}