using Smartwyre.DeveloperTest.Application.Models;

namespace Smartwyre.DeveloperTest.Application.Interfaces;

public interface IRebateService
{
    CalculateRebateResult CalculateAndStore(CalculateRebateRequest request);
}
