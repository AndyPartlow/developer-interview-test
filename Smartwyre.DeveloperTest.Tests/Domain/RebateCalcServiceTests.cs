using System.Collections.Generic;
using Xunit;
using Smartwyre.DeveloperTest.Domain.Enums;
using Smartwyre.DeveloperTest.Domain.Models;
using Smartwyre.DeveloperTest.Domain.Services;

namespace Smartwyre.DeveloperTest.Tests.Domain;

public class RebateCalcServiceTests
{
    [Theory]
    [MemberData(nameof(TestData_CalculateRebate))]
    public void CalculateRebate_GeneratesCorrectResult(Rebate rebate, Product product, decimal volume, bool expectedSuccess, decimal expectedRebateAmount)
    {
        var rebateCalcService = new RebateCalcService();

        var actualSuccess = rebateCalcService.CalculateRebate(rebate, product, volume, out var actualRebateAmount);

        Assert.Equal(expectedSuccess, actualSuccess);
        Assert.Equal(expectedRebateAmount, actualRebateAmount);
    }

    public static IEnumerable<object[]> TestData_CalculateRebate()
    {
        yield return new object[]
        {
            new Rebate(),
            new Product(),
            0,
            false,
            0
        };

        yield return new object[]
        {
            new Rebate { Identifier = "Rebate1", Incentive = IncentiveType.FixedRateRebate, Amount = 3, Percentage = 50},
            new Product { Identifier = "Product2", SupportedIncentives = new[] { IncentiveType.FixedRateRebate, IncentiveType.FixedCashAmount }, Price = 20, Uom = "Uom1" },
            10,
            true,
            10000
        };
    }
}
