﻿# nullable disable
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
    public Application(ILogger<Application> logger)
    {
        _logger = logger;
    }

    public void Process()
    {
        string[] lines = System.IO.File.ReadAllLines(@"./IO/input.txt");

        foreach (var line in lines)
        {
        }

        Console.Write("Press any key to exit.");
        Console.ReadKey();
    }
}