using Microsoft.EntityFrameworkCore;
using PetMarket.Models;
using PetMarket.Repositories.Interfaces;

namespace PetMarket.Repositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        public PetRepository(PetMarketDbContext context) : base(context) { }

        
    }
}