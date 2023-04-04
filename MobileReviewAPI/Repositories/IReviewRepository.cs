using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviews();
        Task <Review>GetReviewById(int id);
        Task<IEnumerable<Review>>GetMobileReviews(int mobId);
        bool ReviewExists(int reviewId);
    }
}
