using Smartwyre.DeveloperTest.Domain.Models;

namespace Smartwyre.DeveloperTest.Domain.Interfaces;

public interface IProductRepository
{
    Product GetProduct(string productIdentifier);
}
