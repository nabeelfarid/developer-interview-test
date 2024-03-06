using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateCalculatorCreator : IRebateCalculatorCreator
{
    public IRebateCalculator Create(IncentiveType? incentiveType)
    {
        return incentiveType switch
        {
            IncentiveType.FixedCashAmount => new FixedCashAmountRebateCalculator(),
            IncentiveType.FixedRateRebate => new FixedRateRebateCalculator(),
            IncentiveType.AmountPerUom => new AmountPerUomRebateCalculator(),
            _ => null,
        };
    }
}
