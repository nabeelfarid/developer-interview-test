using System;
using Xunit;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests;

public class AmountPerUomRebateCalculatorTests : IDisposable
{
    private readonly IRebateCalculator _rebateCalculator;

    // setup
    public AmountPerUomRebateCalculatorTests()
    {
        //Arrange
        _rebateCalculator = new AmountPerUomRebateCalculator();
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
        var product = new Product() { SupportedIncentives = SupportedIncentiveType.AmountPerUom, };
        var rebate = new Rebate() { Amount = 15, };

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
        Assert.Equal(response.RebateAmount, rebate.Amount * request.Volume);
    }
}
