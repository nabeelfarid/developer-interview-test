using System;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        // Set up the Dependency Injection Container
        var serviceProvider = new ServiceCollection()
            .AddTransient<IProductDataStore, ProductDataStore>()
            .AddTransient<IRebateDataStore, RebateDataStore>()
            .AddTransient<IRebateCalculatorCreator, RebateCalculatorCreator>()
            .AddTransient<IRebateService, RebateService>()
            .BuildServiceProvider();

        // Create instance of RebateService with all the dependencies
        var rebateService = serviceProvider.GetService<IRebateService>();

        // Prompt user for rebateIdentifier, productIdentifier, and volume
        Console.Write("Enter rebate Identifier: ");
        string rebateIdentifier = Console.ReadLine();

        Console.Write("Enter product Identifier: ");
        string productIdentifier = Console.ReadLine();

        Console.Write("Enter volume: ");
        int volume = int.Parse(Console.ReadLine()); // Assuming input is valid integer

        // Create CalculateRebateRequest instance with user input
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebateIdentifier,
            ProductIdentifier = productIdentifier,
            Volume = volume
        };

        // Call RebateService to calculate rebate
        var result = rebateService.Calculate(request);

        // Display result
        Console.WriteLine($"Rebate calculation result: {(result.Success ? "Success" : "Failure")}");

        Console.ReadLine(); // Wait for user input before closing console
    }
}
