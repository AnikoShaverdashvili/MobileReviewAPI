using MobileReviewAPI.Models;

namespace MobileReviewAPI.Interfaces
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllOwnersAsync();
        Task<Owner> GetOwnerById(int id);
        Task<IEnumerable<Owner>> GetOwnerOfMobile(int mobId);
        Task<IEnumerable<Mobile>> GetMobileByOwner(int ownerId);
        bool OwnerExists(int ownerId);
        Task<bool> CreateOwner(Owner owner);
        Task<bool> Save();

        //Update
        Task<bool> UpdateOwner(Owner owner);
        //Delete
        Task<bool> DeleteOwner(Owner owner);

    }
}
