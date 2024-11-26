using Moq;
using Smartwyre.DeveloperTest.Application.Models;
using Smartwyre.DeveloperTest.Domain.Enums;
using Smartwyre.DeveloperTest.Domain.Interfaces;
using Smartwyre.DeveloperTest.Domain.Models;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Application;

public class RebateServiceTests
{
    [Fact]
    public void CalculateAndStore_ValidRequest_ReturnsUnsuccessfulResult()
    {
        CalculateRebateRequest rebateRequest = new CalculateRebateRequest
        {
            RebateIdentifier = "Rebate11",
            ProductIdentifier = "Product22",
            Volume = 999
        };

        var expectedRebateResult = new CalculateRebateResult { Success = true, Amount = 987 };

        var mockRebateRepository = new Mock<IRebateRepository>();
        var mockProductRepository = new Mock<IProductRepository>();
        var mockRebateCalcService = new Mock<IRebateCalcService>();

        var mockRebate = new Rebate { Identifier = rebateRequest.RebateIdentifier, Incentive = IncentiveType.FixedRateRebate, Amount = 99, Percentage = 15 };
        mockRebateRepository
            .Setup(x => x.GetRebate(rebateRequest.RebateIdentifier))
            .Returns(mockRebate)
            .Verifiable();
        mockRebateRepository
            .Setup(x => x.StoreCalculationResult(mockRebate, 987))
            .Returns(true)
            .Verifiable();

        var mockProduct = new Product { Identifier = rebateRequest.ProductIdentifier, Price = 25, SupportedIncentives = new[] { IncentiveType.FixedRateRebate }, Uom = "Uom1" };
        mockProductRepository
            .Setup(x => x.GetProduct(rebateRequest.ProductIdentifier))
            .Returns(mockProduct)
            .Verifiable();

        decimal calculatedRebateAmount = expectedRebateResult.Amount;
        mockRebateCalcService
            .Setup(x => x.CalculateRebate(mockRebate, mockProduct, rebateRequest.Volume, out calculatedRebateAmount))
            .Returns(expectedRebateResult.Success)
            .Verifiable();

        var rebateService = new RebateService(mockRebateRepository.Object, mockProductRepository.Object, mockRebateCalcService.Object);

        var actualRebateResult = rebateService.CalculateAndStore(rebateRequest);

        Assert.Equal(expectedRebateResult.Success, actualRebateResult.Success);
        Assert.Equal(expectedRebateResult.Amount, actualRebateResult.Amount);

        mockRebateRepository.Verify();
        mockProductRepository.Verify();
        mockRebateCalcService.Verify();
    }
}