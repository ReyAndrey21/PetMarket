using PetMarket.Models;

namespace PetMarket.Services.Interfaces
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> GetAllPetsAsync();
        Task<Pet?> GetPetByIdAsync(int id);
        Task<bool> CreatePetAsync(Pet pet);
        Task<bool> UpdatePetAsync(Pet pet);
        Task<bool> DeletePetAsync(int id);
      
    }
}