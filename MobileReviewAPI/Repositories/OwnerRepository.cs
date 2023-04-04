using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
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
    }
}
