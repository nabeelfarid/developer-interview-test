using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateCalculatorCreator
{
    public IRebateCalculator Create(IncentiveType? incentiveType);
}
