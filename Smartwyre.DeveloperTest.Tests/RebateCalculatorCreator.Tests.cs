using System;
using Xunit;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateCalculatorCreatorTests : IDisposable
{
    private readonly RebateCalculatorCreator _rebateCalculatorCreator;

    // setup
    public RebateCalculatorCreatorTests()
    {
        //Arrange
        _rebateCalculatorCreator = new RebateCalculatorCreator();
    }

    // teardown
    public void Dispose()
    {
        // Dispose here
    }

    [Fact]
    public void Create_GivenIncentiveTypeOfFixedCashAmount_ShouldReturnFixedCashAmountRebateCalculator()
    {
        //Act
        var calculator = _rebateCalculatorCreator.Create(IncentiveType.FixedCashAmount);

        //Assert
        Assert.IsType<FixedCashAmountRebateCalculator>(calculator);
    }

    [Fact]
    public void Create_GivenIncentiveTypeOfFixedRateRebate_ShouldReturnFixedRateRebateCalculator()
    {
        //Act
        var calculator = _rebateCalculatorCreator.Create(IncentiveType.FixedRateRebate);

        //Assert
        Assert.IsType<FixedRateRebateCalculator>(calculator);
    }

    [Fact]
    public void Create_GivenIncentiveTypeOfAmountPerUom_ShouldReturnAmountPerUomRebateCalculator()
    {
        //Act
        var calculator = _rebateCalculatorCreator.Create(IncentiveType.AmountPerUom);

        //Assert
        Assert.IsType<AmountPerUomRebateCalculator>(calculator);
    }

    [Fact]
    public void Create_GivenIncentiveTypeOfNull_ShouldReturnNull()
    {
        //Act
        var calculator = _rebateCalculatorCreator.Create(null);

        //Assert
        Assert.Null(calculator);
    }
}
