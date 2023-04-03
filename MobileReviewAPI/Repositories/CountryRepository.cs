using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly MobileReviewDbContext _context;

        public CountryRepository(MobileReviewDbContext context)
        {
            _context = context;
        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _context.Countries.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Country> GetCountryById(int id)
        {
            return await _context.Countries.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Country> GetCountryByOwner(int id)
        {
            return await _context.Owners.Where(o => o.Id == id).Select(c => c.Country).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Owner>> GetOwnersFromCountry(int id)
        {
            return await _context.Owners.Where(c => c.Country.Id == id).ToListAsync();
        }
    }
}
