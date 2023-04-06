using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.Interfaces;
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

        public async Task<bool> CreateCountry(Country country)
        {
            await _context.Countries.AddAsync(country);
            return await Save();
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
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

        public async Task<bool> Save()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 ? true : false;
        }
        public async Task<bool> UpdateCountry(Country country)
        {
            _context.Update(country);
            return await Save();
        }
        public async Task<bool> DeleteCountry(Country country)
        {
            _context.Remove(country);
            return await Save();
        }
    }
}
