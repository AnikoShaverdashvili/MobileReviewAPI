using MobileReviewAPI.Models;

namespace MobileReviewAPI.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviews();
        Task<Review> GetReviewById(int id);
        Task<IEnumerable<Review>> GetMobileReviews(int mobId);
        bool ReviewExists(int reviewId);
        Task<bool> CreateReview(Review review);
        Task<bool> Save();
        Task<bool> UpdateReview(Review review);

        //Delete
        Task<bool> DeleteReview(Review review);
        Task<bool> DeleteReviewList(List<Review> reviews);

    }
}
