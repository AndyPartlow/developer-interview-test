using Smartwyre.DeveloperTest.Domain.Interfaces;
using Smartwyre.DeveloperTest.Domain.Models;

namespace Smartwyre.DeveloperTest.Infrastructure.Repositories;

public class ProductDataStore : IProductRepository
{
    public Product GetProduct(string productIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return new Product();
    }
}
