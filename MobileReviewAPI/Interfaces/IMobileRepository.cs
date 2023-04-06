using MobileReviewAPI.Models;

namespace MobileReviewAPI.Interfaces
{
    public interface IMobileRepository
    {
        Task<IEnumerable<Mobile>> GetAllMobileAsync();
        Task<Mobile> GetMobileIdAsync(int mobId);
        Task<Mobile> GetMobileNamesAsync(string Name);
        Task<decimal> GetMobileRating(int mobId);
        bool MobileExists(int mobId);
        Task<bool> CreateMobile(int ownerId, int categoryId, Mobile mobile);
        Task<bool> Save();
        //Update
        Task<bool> UpdateMobile(int ownerId, int categoryId, Mobile mobile);
        //Delete
        Task<bool> DeleteMobile(Mobile mobile);
    }
}
