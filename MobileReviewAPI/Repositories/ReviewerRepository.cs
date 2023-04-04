using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly MobileReviewDbContext _context;

        public ReviewerRepository(MobileReviewDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reviewer>> GetAllReviewers()
        {
            return await _context.Reviewers.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<Reviewer> GetReviewerById(int id)
        {
            return await _context.Reviewers.Where(r => r.Id == id).Include(e => e.Reviews).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByReviewer(int reviewerId)
        {
            return await _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToListAsync();
        }

        public bool ReviwerExists(int id)
        {
            return _context.Reviewers.Any(r => r.Id == id);
        }
    }
}
