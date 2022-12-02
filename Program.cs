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
        Stack<decimal> topCalories = new Stack<decimal>();
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
                    topCalories.Push(maxCalories);
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
            topCalories.Push(maxCalories);
        }

        Decimal result;
        result = SumTopThree(topCalories);
        Decimal result1 = allCaloriesList.OrderBy(inP => -inP).Take(3).Sum();
        Console.WriteLine($"Calories carried by the 3 elves with the most by stack: {result}");
        Console.WriteLine($"Calories carried by the 3 elves with the most by List: {result1}");
        Console.Write("Press any key to exit.");
        Console.ReadKey();
    }

    private Decimal SumTopThree(Stack<Decimal> inP)
    {
        var flagIncluded = (int count) => count < 3 ? "*" : "";
        Decimal result = 0;
        int count = 0;
        Decimal tmp;
        while (inP.TryPop(out tmp))
        {
            _logger.LogInformation($"{tmp}{flagIncluded(count)}");
            if (count < 3)
            {
                result +=tmp;
                count ++;
            }
        }
        return result;
    }
}