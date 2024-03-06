using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class AmountPerUomRebateCalculator : IRebateCalculator
{
    public CalculateRebateResult Calculate(
        Rebate rebate,
        Product product,
        CalculateRebateRequest request
    )
    {
        CalculateRebateResult result = new();
        if (rebate == null)
        {
            result.Success = false;
        }
        else if (product == null)
        {
            result.Success = false;
        }
        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
        {
            result.Success = false;
        }
        else if (rebate.Amount == 0 || request.Volume == 0)
        {
            result.Success = false;
        }
        else
        {
            result.RebateAmount = rebate.Amount * request.Volume;
            result.Success = true;
        }
        return result;
    }
}
