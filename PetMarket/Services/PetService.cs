
using PetMarket.Models;
using PetMarket.Repositories.Interfaces;
using PetMarket.Services.Interfaces;
using System.Threading.Tasks;

namespace PetMarket.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;

        }

        public async Task<IEnumerable<Pet>> GetAllPetsAsync()
        {

            return await _petRepository.GetAllAsync();
        }

        public async Task<Pet?> GetPetByIdAsync(int id)
        {
            return await _petRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreatePetAsync(Pet pet)
        {

            await _petRepository.AddAsync(pet);
            return await _petRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePetAsync(Pet pet)
        {
            var existingPet = await _petRepository.GetByIdAsync(pet.IdPet);
            if (existingPet == null) return false;

            existingPet.TypePet = pet.TypePet;
            existingPet.ImagePetUrl = pet.ImagePetUrl;

            _petRepository.Update(existingPet);
            return await _petRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePetAsync(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null) return false;


            _petRepository.Remove(pet);
            return await _petRepository.SaveChangesAsync() > 0;
        }

    }
}