using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.Models;
using System.Text.RegularExpressions;

namespace MobileReviewAPI.Repositories
{
    public class MobileRepository : IMobileRepository
    {
        private readonly MobileReviewDbContext _context;

        public MobileRepository(MobileReviewDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mobile>> GetAllMobileAsync()
        {
            return await _context.Mobiles.OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<Mobile> GetMobileIdAsync(int mobId)
        {
            return await _context.Mobiles.Where(m => m.Id == mobId).FirstOrDefaultAsync();
        }

        public async Task<Mobile> GetMobileNamesAsync(string name)
        {
            return await _context.Mobiles.Where(m => m.Name.Trim() == name.Trim()).FirstOrDefaultAsync();
        }

        public async Task<decimal> GetMobileRating(int mobId)
        {
            var review = _context.Reviews.Where(m => m.Mobile.Id == mobId);
            if (review.Count() <= 0)
            {
                return 0;
            }
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public async Task<bool> MobileExists(int mobId)
        {
            return _context.Mobiles.Any(m => m.Id == mobId);
        }
    }
}
