using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public interface IMobileRepository
    {
        Task<IEnumerable<Mobile>> GetAllMobileAsync();
        Task<Mobile> GetMobileIdAsync(int mobId);
        Task<Mobile> GetMobileNamesAsync(string Name);
        Task<decimal> GetMobileRating(int mobId);
        bool MobileExists(int mobId);
    }
}
