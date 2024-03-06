using System;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IRebateCalculatorCreator _rebateCalculatorCreator;

    public RebateService(
        IProductDataStore productDataStore,
        IRebateDataStore rebateDataStore,
        IRebateCalculatorCreator rebateCalculatorCreator
    )
    {
        _rebateCalculatorCreator =
            rebateCalculatorCreator
            ?? throw new ArgumentNullException(nameof(rebateCalculatorCreator));
        _productDataStore =
            productDataStore ?? throw new ArgumentNullException(nameof(productDataStore));
        _rebateDataStore =
            rebateDataStore ?? throw new ArgumentNullException(nameof(rebateDataStore));
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request.ProductIdentifier);

        IRebateCalculator rebateCalculator = _rebateCalculatorCreator.Create(rebate?.Incentive);

        var result =
            rebateCalculator == null ? new() : rebateCalculator.Calculate(rebate, product, request);
        if (result.Success)
        {
            _rebateDataStore.StoreCalculationResult(rebate, result.RebateAmount.Value);
            result.Success = true;
        }

        return result;
    }
}
