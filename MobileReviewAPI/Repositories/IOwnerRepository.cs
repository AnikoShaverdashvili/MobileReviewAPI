using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllOwnersAsync();
        Task<Owner> GetOwnerById(int id);
        Task<IEnumerable<Owner>> GetOwnerOfMobile(int mobId);
        Task<IEnumerable<Mobile>> GetMobileByOwner(int ownerId);
        bool OwnerExists(int ownerId);  

    }
}
