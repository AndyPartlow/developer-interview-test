using Smartwyre.DeveloperTest.Domain.Models;

namespace Smartwyre.DeveloperTest.Domain.Interfaces;

public interface IRebateCalcService
{
    bool CalculateRebate(Rebate rebate, Product product, decimal volume, out decimal rebateAmount);
}
