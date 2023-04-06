using MobileReviewAPI.Models;

namespace MobileReviewAPI.Interfaces
{
    public interface IReviewerRepository
    {
        Task<IEnumerable<Reviewer>> GetAllReviewers();
        Task<Reviewer> GetReviewerById(int id);
        Task<IEnumerable<Review>> GetReviewsByReviewer(int reviewerId);
        bool ReviwerExists(int id);
        //Create Section
        Task<bool> CreateReviewer(Reviewer reviewer);
        Task<bool> Save();

        Task<bool> UpdateReviewer(Reviewer reviewer);
        Task<bool> DeleteReviewer(Reviewer reviewer);


    }
}
