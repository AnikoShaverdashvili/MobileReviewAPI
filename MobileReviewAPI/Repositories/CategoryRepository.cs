using Microsoft.EntityFrameworkCore;
using MobileReviewAPI.Data;
using MobileReviewAPI.Interfaces;
using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MobileReviewDbContext _context;

        public CategoryRepository(MobileReviewDbContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public async Task<bool> CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            return await Save();
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            _context.Remove(category);
            return await Save();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Mobile>> GetMobileByCategory(int categoryId)
        {
            return await _context.MobileCategories.Where(mc => mc.CategoryId == categoryId).Select(m => m.Mobile).ToListAsync();
        }

        public async Task<bool> Save()
        {

            var save = await _context.SaveChangesAsync();

            return save > 0 ? true : false;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            _context.Update(category);
            return await Save();
        }
    }
}
