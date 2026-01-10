using PetMarket.Models;

namespace PetMarket.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetAllUsersSortedAsync();
        Task<User?> GetUserByIdAsync(string id);
    }
}