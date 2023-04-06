using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.Interfaces;
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

        public async Task<bool> CreateReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            return await Save();
        }

        public async Task<bool> DeleteReview(Review review)
        {
            _context.Remove(review);
            return await Save();
        }

        public async Task<bool> DeleteReviewList(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return await Save();
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

        public async Task<bool> Save()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 ? true : false;
        }

        public async Task<bool> UpdateReview(Review review)
        {
            _context.Update(review);
            return await Save();
        }
    }
}
