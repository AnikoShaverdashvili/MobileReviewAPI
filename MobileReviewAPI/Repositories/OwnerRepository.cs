using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.Interfaces;
using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly MobileReviewDbContext _context;

        public OwnerRepository(MobileReviewDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOwner(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            return await Save();
        }

        public async Task<bool> DeleteOwner(Owner owner)
        {
            _context.Update(owner);
            return await Save();
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _context.Owners.OrderBy(o => o.Id).ToListAsync();
        }

        public async Task<IEnumerable<Mobile>> GetMobileByOwner(int ownerId)
        {
            return await _context.MobileOwners.Where(o => o.Owner.Id == ownerId).Select(m => m.Mobile).ToListAsync();
        }

        public async Task<Owner> GetOwnerById(int id)
        {
            return await _context.Owners.Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Owner>> GetOwnerOfMobile(int mobId)
        {
            return await _context.MobileOwners.Where(x => x.Mobile.Id == mobId).Select(o => o.Owner).ToListAsync();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }

        public async Task<bool> Save()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 ? true : false;
        }
        public async Task<bool> UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return await Save();
        }
    }
}
