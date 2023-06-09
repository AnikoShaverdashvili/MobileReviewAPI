﻿using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.Interfaces;
using MobileReviewAPI.Models;
using System.Text.RegularExpressions;

namespace MobileReviewAPI.Repositories
{
    public class MobileRepository : IMobileRepository
    {
        private readonly MobileReviewDbContext _context;

        public MobileRepository(MobileReviewDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMobile(int ownerId, int categoryId, Mobile mobile)
        {
            var mobileOwnerEntity = await _context.Owners.Where(o => o.Id == ownerId).FirstOrDefaultAsync();
            var category = await _context.Categories.Where(c => c.Id == categoryId).FirstOrDefaultAsync();
            var mobileOwner = new MobileOwner()
            {
                Owner = mobileOwnerEntity,
                Mobile = mobile,
            };
            _context.AddAsync(mobileOwner);

            var mobileCategory = new MobileCategory()
            {
                Category = category,
                Mobile = mobile,
            };
            _context.AddAsync(mobileCategory);
            _context.AddAsync(mobile);
            return await Save();
        }

        public async Task<bool> DeleteMobile(Mobile mobile)
        {
            _context.Remove(mobile);
            return await Save();
        }

        public async Task<IEnumerable<Mobile>> GetAllMobileAsync()
        {
            return await _context.Mobiles.OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<Mobile> GetMobileIdAsync(int mobId)
        {
            return await _context.Mobiles.Where(m => m.Id == mobId).FirstOrDefaultAsync();
        }

        public async Task<Mobile> GetMobileNamesAsync(string name)
        {
            return await _context.Mobiles.Where(m => m.Name.Trim() == name.Trim()).FirstOrDefaultAsync();
        }

        public async Task<decimal> GetMobileRating(int mobId)
        {
            var review = _context.Reviews.Where(m => m.Mobile.Id == mobId);
            if (review.Count() <= 0)
            {
                return 0;
            }
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public bool MobileExists(int mobId)
        {
            return _context.Mobiles.Any(m => m.Id == mobId);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateMobile(int ownerId, int categoryId, Mobile mobile)
        {
            _context.Update(mobile);
            return await Save();
        }
    }
}
