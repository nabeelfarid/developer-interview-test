using System;
using Xunit;
using Moq;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests : IDisposable
{
    private readonly Mock<IProductDataStore> _mockProductDataStore;
    private readonly Mock<IRebateDataStore> _mockRebateDataStore;
    private readonly Mock<IRebateCalculatorCreator> _mockRebateCalculatorCreator;
    private readonly Mock<IRebateCalculator> _mockRebateCalculator;

    private readonly RebateService _rebateService;

    // setup
    public RebateServiceTests()
    {
        //Arrange
        _mockProductDataStore = new Mock<IProductDataStore>();
        _mockRebateDataStore = new Mock<IRebateDataStore>();
        _mockRebateCalculatorCreator = new Mock<IRebateCalculatorCreator>();
        _mockRebateCalculator = new Mock<IRebateCalculator>();
        _rebateService = new RebateService(
            _mockProductDataStore.Object,
            _mockRebateDataStore.Object,
            _mockRebateCalculatorCreator.Object
        );
    }

    // teardown
    public void Dispose()
    {
        // Dispose here
    }

    [Fact]
    public void CalculateRebate_GivenProductAndRebateWithValidIncentive_ShouldCalculateAndReturnRebateAmount()
    {
        //Arrange
        var product = new Product() { Identifier = "p1", };
        var rebate = new Rebate() { Identifier = "r1", Incentive = IncentiveType.FixedCashAmount, };
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = product.Identifier,
            RebateIdentifier = rebate.Identifier
        };
        var expectedResult = new CalculateRebateResult() { Success = true, RebateAmount = 10 };

        _mockProductDataStore.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(product);
        _mockRebateDataStore.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(rebate);
        _mockRebateCalculatorCreator
            .Setup(x => x.Create(rebate.Incentive))
            .Returns(_mockRebateCalculator.Object);
        _mockRebateCalculator
            .Setup(x => x.Calculate(rebate, product, request))
            .Returns(expectedResult);
        _mockRebateDataStore.Setup(
            x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>())
        );

        //Act
        var actualResult = _rebateService.Calculate(request);

        //Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void CalculateRebate_GivenProductAndRebateWithInValidIncentive_ShouldNeitherCalculateNorReturnRebateAmount()
    {
        //Arrange
        var product = new Product() { Identifier = "p1" };
        var rebate = new Rebate() { Identifier = "r1" };
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = product.Identifier,
            RebateIdentifier = rebate.Identifier
        };

        _mockProductDataStore.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(product);
        _mockRebateDataStore.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(rebate);
        _mockRebateCalculatorCreator
            .Setup(x => x.Create(rebate.Incentive))
            .Returns<IRebateCalculator>(null);
        _mockRebateDataStore.Setup(
            x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>())
        );

        //Act
        var result = _rebateService.Calculate(request);

        //Assert
        Assert.False(result.Success);
        Assert.Null(result.RebateAmount);
        // no calculation
        _mockRebateCalculator.Verify(
            x =>
                x.Calculate(
                    It.IsAny<Rebate>(),
                    It.IsAny<Product>(),
                    It.IsAny<CalculateRebateRequest>()
                ),
            Times.Never
        );
        // no storing of data
        _mockRebateDataStore.Verify(
            x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()),
            Times.Never
        );
    }
}
