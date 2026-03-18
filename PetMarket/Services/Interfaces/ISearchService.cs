using PetMarket.Models;

namespace PetMarket.Services.Interfaces
{
    public interface ISearchService
    {
        List<Product> Search(List<Product> allProducts, string query);
    }
}