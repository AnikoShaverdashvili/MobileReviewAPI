using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public interface IReviewerRepository
    {
        Task<IEnumerable<Reviewer>> GetAllReviewers();
        Task<Reviewer> GetReviewerById(int id);
        Task<IEnumerable<Review>> GetReviewsByReviewer(int reviewerId);
        bool ReviwerExists(int id);

    }
}
