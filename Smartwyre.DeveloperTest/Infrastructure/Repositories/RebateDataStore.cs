using Smartwyre.DeveloperTest.Domain.Interfaces;
using Smartwyre.DeveloperTest.Domain.Models;

namespace Smartwyre.DeveloperTest.Infrastructure.Repositories;

public class RebateDataStore : IRebateRepository
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return new Rebate();
    }

    public bool StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
        return true;
    }
}
