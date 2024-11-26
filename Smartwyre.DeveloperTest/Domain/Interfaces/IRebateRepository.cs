using Smartwyre.DeveloperTest.Domain.Models;

namespace Smartwyre.DeveloperTest.Domain.Interfaces;

public interface IRebateRepository
{
    Rebate GetRebate(string rebateIdentifier);

    bool StoreCalculationResult(Rebate account, decimal rebateAmount);
}
