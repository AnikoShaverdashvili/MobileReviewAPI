using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MobileReviewDbContext _context;

        public ReviewRepository(MobileReviewDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _context.Reviews.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetMobileReviews(int mobId)
        {
            return await _context.Reviews.Where(r => r.Mobile.Id == mobId).ToListAsync();
        }

        public async Task<Review> GetReviewById(int id)
        {
            return await _context.Reviews.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviewers.Any(r=>r.Id==reviewId);  
        }
    }
}
