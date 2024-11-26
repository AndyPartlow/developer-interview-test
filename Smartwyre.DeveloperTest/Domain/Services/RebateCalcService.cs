using Smartwyre.DeveloperTest.Domain.Enums;
using Smartwyre.DeveloperTest.Domain.Interfaces;
using Smartwyre.DeveloperTest.Domain.Models;
using System.Linq;

namespace Smartwyre.DeveloperTest.Domain.Services;

public class RebateCalcService : IRebateCalcService
{
    public bool CalculateRebate(Rebate rebate, Product product, decimal volume, out decimal rebateAmount)
    {
        rebateAmount = 0m;

        if (rebate is null || product is null)
        {
            return false;
        }

        if (product.SupportedIncentives?.All(x => x != rebate.Incentive) ?? true)
        {
            return false;
        }

        switch (rebate.Incentive)
        {
            case IncentiveType.FixedCashAmount:
                if (rebate.Amount == 0)
                {
                    return false;
                }

                rebateAmount = rebate.Amount;

                break;

            case IncentiveType.FixedRateRebate:
                if (rebate.Percentage == 0 || product.Price == 0 || volume == 0)
                {
                    return false;
                }

                rebateAmount = product.Price * rebate.Percentage * volume;

                break;

            case IncentiveType.AmountPerUom:
                if (rebate.Amount == 0 || volume == 0)
                {
                    return false;
                }

                rebateAmount = rebate.Amount * volume;

                break;
        }

        return true;
    }
}
