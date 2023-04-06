using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.Interfaces;
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

        public async Task<bool> CreateReviewer(Reviewer reviewer)
        {
            await _context.Reviewers.AddAsync(reviewer);
            return await Save();
        }

        public async Task<bool> DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return await Save();
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

        public async Task<bool> Save()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 ? true : false;
        }

        public async Task<bool> UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return await Save();
        }
    }
}
