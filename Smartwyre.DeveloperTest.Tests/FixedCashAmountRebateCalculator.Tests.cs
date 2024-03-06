using System;
using Xunit;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests;

public class FixedCashAmountRebateCalculatorTests : IDisposable
{
    private readonly IRebateCalculator _rebateCalculator;

    // setup
    public FixedCashAmountRebateCalculatorTests()
    {
        //Arrange
        _rebateCalculator = new FixedCashAmountRebateCalculator();
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
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };
        var rebate = new Rebate() { Amount = 10, };

        CalculateRebateRequest request =
            new() { ProductIdentifier = product.Identifier, RebateIdentifier = rebate.Identifier };

        //Act
        var response = _rebateCalculator.Calculate(rebate, product, request);

        //Assert
        Assert.True(response.Success);
        Assert.Equal(response.RebateAmount, rebate.Amount);
    }
}
