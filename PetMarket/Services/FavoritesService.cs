using PetMarket.Models;
using PetMarket.Repositories.Interfaces;
using PetMarket.Services.Interfaces;

namespace PetMarket.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IFavoritesRepository _favoritesRepository;

        public FavoritesService(IFavoritesRepository favoritesRepository)
        {
            _favoritesRepository = favoritesRepository;
        }

        public async Task<bool> ToggleFavoriteAsync(string userId, int productId)
        {
            // Căutăm dacă produsul este deja la favorite
            var existing = await _favoritesRepository.GetByUserAndProductAsync(userId, productId);

            if (existing != null)
            {
                // Folosim 'Remove' așa cum este definit în IRepository
                _favoritesRepository.Remove(existing);

                // Trebuie să apelăm SaveChangesAsync pentru a persista modificarea în baza de date
                await _favoritesRepository.SaveChangesAsync();
                return false; // Indicăm că a fost eliminat
            }

            // Dacă nu există, îl adăugăm
            var favorite = new Favorites
            {
                UserId = userId,
                IdProduct = productId
            };

            await _favoritesRepository.AddAsync(favorite); //
            await _favoritesRepository.SaveChangesAsync(); //
            return true; // Indicăm că a fost adăugat
        }

        public async Task<IEnumerable<Favorites>> GetUserFavoritesAsync(string userId)
        {
            return await _favoritesRepository.GetByUserIdAsync(userId);
        }

        public async Task<bool> IsFavoriteAsync(string userId, int productId)
        {
            var fav = await _favoritesRepository.GetByUserAndProductAsync(userId, productId);
            return fav != null;
        }
    }
}