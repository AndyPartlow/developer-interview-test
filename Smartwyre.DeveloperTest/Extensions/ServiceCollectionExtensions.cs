using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Application.Interfaces;
using Smartwyre.DeveloperTest.Domain.Interfaces;
using Smartwyre.DeveloperTest.Domain.Services;
using Smartwyre.DeveloperTest.Infrastructure.Repositories;

namespace Smartwyre.DeveloperTest.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IProductRepository, ProductDataStore>()
            .AddScoped<IRebateRepository, RebateDataStore>();
    }

    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services.AddScoped<IRebateCalcService, RebateCalcService>();
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services.AddScoped<IRebateService, RebateService>();
    }
}
