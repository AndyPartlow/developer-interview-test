using Smartwyre.DeveloperTest.Application.Interfaces;
using Smartwyre.DeveloperTest.Application.Models;
using Smartwyre.DeveloperTest.Domain.Interfaces;
using Smartwyre.DeveloperTest.Domain.Models;

public class RebateService : IRebateService
{
    private readonly IRebateRepository _rebateRepository;
    private readonly IProductRepository _productRepository;
    private readonly IRebateCalcService _rebateCalcService;

    public RebateService(IRebateRepository rebateRepository, IProductRepository productRepository, IRebateCalcService rebateCalcService)
    {
        _rebateRepository = rebateRepository;
        _productRepository = productRepository;
        _rebateCalcService = rebateCalcService;
    }

    public CalculateRebateResult CalculateAndStore(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateRepository.GetRebate(request.RebateIdentifier);
        Product product = _productRepository.GetProduct(request.ProductIdentifier);

        var calcSuccess = _rebateCalcService.CalculateRebate(rebate, product, request.Volume, out var rebateAmount);

        if (!calcSuccess)
        {
            return new CalculateRebateResult { Success = false };
        }

        var persistenceSuccess = _rebateRepository.StoreCalculationResult(rebate, rebateAmount);

        return new CalculateRebateResult { Success = persistenceSuccess, Amount = rebateAmount};
    }
}
