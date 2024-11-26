using Microsoft.Extensions.DependencyInjection;
using System;
using Smartwyre.DeveloperTest.Extensions;
using Smartwyre.DeveloperTest.Application.Interfaces;
using Smartwyre.DeveloperTest.Application.Models;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddDomainServices()
            .AddInfrastructureServices()
            .AddApplicationServices()
            .BuildServiceProvider();

        if (args == null || args.Length != 3 || args[0].Length == 0 || args[1].Length == 0 || !int.TryParse(args[2], out var volume))
        {
            Console.WriteLine("Format is SmartWyre.Developer.TestRunner RebateID, ProductID, Volume");
            return;
        }

        var rebateService = serviceProvider.GetService<IRebateService>();

        var rebateRequest = new CalculateRebateRequest 
        { 
            RebateIdentifier = args[0], 
            ProductIdentifier = args[1], 
            Volume = volume 
        };

        var rebateResult = rebateService.CalculateAndStore(rebateRequest);

        Console.WriteLine($"Rebate result: Success={rebateResult.Success}; Amount={rebateResult.Amount}");
    }
}
