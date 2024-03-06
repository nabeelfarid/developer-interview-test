using System;
using Xunit;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests;

public class FixedRateRebateCalculatorTests : IDisposable
{
    private readonly IRebateCalculator _rebateCalculator;

    // setup
    public FixedRateRebateCalculatorTests()
    {
        //Arrange
        _rebateCalculator = new FixedRateRebateCalculator();
    }

    // teardown
    public void Dispose()
    {
        // Dispose here
    }

    [Fact]
    public void CalculateRebate_GivenProductAndRebate_ShouldCalculateRebateAmount()
    {
        //Arrange
        var product = new Product()
        {
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 20
        };
        var rebate = new Rebate() { Percentage = 15, };

        CalculateRebateRequest request =
            new()
            {
                ProductIdentifier = product.Identifier,
                RebateIdentifier = rebate.Identifier,
                Volume = 10
            };

        //Act
        var response = _rebateCalculator.Calculate(rebate, product, request);

        //Assert
        Assert.True(response.Success);
        Assert.Equal(response.RebateAmount, product.Price * rebate.Percentage * request.Volume);
    }
}
